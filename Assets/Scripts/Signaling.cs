using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signaling : MonoBehaviour
{
    [SerializeField] private Light _light;
    [SerializeField] private float _speedRotate;
    [SerializeField] private float _rateOfChangeSound;

    private float _minimumVolume = 0;
    private float _maximumVolume = 1;
    private AudioSource _sound;

    private Coroutine _coroutine;

    private void Start()
    {
        _sound = GetComponent<AudioSource>();
        _sound.volume = 0;
    }

    private void Update()
    {
        if (_sound.isPlaying)
            _light.transform.RotateAround(_light.transform.position, Vector3.up, _speedRotate * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Thief>(out Thief thief))
            TurnOnAlarm();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Thief>(out Thief thief))
            TurnOffAlarm();
    }

    private void TurnOnAlarm()
    {
        if (_sound.isPlaying == false)
        {
            _light.enabled = true;
            _sound.Play();
        }

        SetVolume(_maximumVolume);
    }

    private void TurnOffAlarm() =>
        SetVolume(_minimumVolume);

    private void SetVolume(float valume)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ÑhangeVolumeSmoothly(valume));
    }

    private IEnumerator ÑhangeVolumeSmoothly(float volume)
    {
        while (_sound.volume != volume)
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, volume, _rateOfChangeSound * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        if (volume == _minimumVolume)
        {
            _sound.Stop();
            _light.enabled = false;
        }
    }
}