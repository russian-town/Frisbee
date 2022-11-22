using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RampMover : MonoBehaviour
{
    public event UnityAction LevelComplete;

    [SerializeField] private float _speed;
    [SerializeField] private float _offSet;
    [SerializeField] private FinalStickman _finalStickman;
    [SerializeField] private LayerMask _coinsLayer;

    private Vector3 _startPosition;
    private Vector3 _previousPosition;
    private Vector3 _targetPosition;
    private List<Vector3> _positions = new List<Vector3>();
    private int _currentPosition;
    private float _distance;
    private bool _canCheckCollision;

    private void Awake()
    {
        _previousPosition = transform.position;
        _distance = transform.localScale.z - _offSet;
    }

    private void OnEnable()
    {
        _finalStickman.TurnEnded += OnTurnEnded;
    }

    private void Start()
    {
        _startPosition = transform.localPosition;
        _targetPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - transform.localScale.z);

        _positions.Add(_startPosition);
        _positions.Add(_targetPosition);

        _currentPosition = 1;
    }

    private void Update()
    {
        if (transform.localPosition != _positions[_currentPosition])
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _positions[_currentPosition], Time.deltaTime * _speed);
        else
            _currentPosition++;

        if (_currentPosition == _positions.Count)
            _currentPosition = 0;
    }

    private void FixedUpdate()
    {
        if (_canCheckCollision == true)
        {
            if (IsCoinCollision(_previousPosition, transform.forward, _distance) == false && IsCoinCollision(transform.position, transform.up, transform.localScale.y) == false)
            {
                LevelComplete?.Invoke();
            }
        }
    }

    private bool IsCoinCollision(Vector3 position, Vector3 direction, float distance)
    {
        return Physics.BoxCast(position, transform.localScale / 2f, direction, transform.rotation, distance, _coinsLayer);
    }

    private void OnTurnEnded(Stickman stickman)
    {
        stickman.TurnEnded -= OnTurnEnded;
        _canCheckCollision = true;
    }
}
