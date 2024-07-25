using UnityEngine;

public class Thief : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;

    private int _currentWaypoint = 0;
    private Vector3 _currentTargetPosition;

    private void Start()
    {
        UpdateCurrentTargetPosition();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentTargetPosition, _speed * Time.deltaTime);

        if (transform.position == _currentTargetPosition)
        {
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Length;
            UpdateCurrentTargetPosition();
        }
    }

    private void UpdateCurrentTargetPosition()
    {
        _currentTargetPosition = _waypoints[_currentWaypoint].position;
        _currentTargetPosition.y = transform.position.y;
    }
}
