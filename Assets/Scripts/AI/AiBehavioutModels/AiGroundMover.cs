using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class AiGroundMover : MonoBehaviour, IActiveAIControllable, IAiObservable, IAiControllable
{
    [SerializeField][Min(0)] private float _stoppingDistance;
    [SerializeField] private GameObjectRotater _objectRotater;

    private Transform _target;
    private Transform _cachedTransform;
    private NavMeshAgent _agent;

    private bool _isStopped;
    private bool _reachStoppingDistance;
    private bool _initialized;
    private bool _isEnabled;

    private const float MINIMAL_MOVE_SPEED = 0.0001f;

    public event Action MoveStarted;
    public event Action MoveEnded;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _cachedTransform = transform;

        _isStopped = true;
        _isEnabled = true;
    }

    public void Initialize(Transform target, float moveSpeed)
    {
        if (_initialized == true)
        {
            return;
        }

        moveSpeed = Mathf.Clamp(moveSpeed, MINIMAL_MOVE_SPEED, float.MaxValue);

        _agent.speed = moveSpeed;
        _target = target;

        _initialized = true;
    }
     
    public void StartMove()
    {
        if (_isEnabled == false)
        {
            return;
        }

        if (_target == null)
        {
            Debug.Log("Can`t move because target is null");

            return;
        }

        if (_reachStoppingDistance == true)
        {
            return;
        }

        _agent.isStopped = false;
        _isStopped = false;

        _agent.SetDestination(_target.position);

        if (_isStopped == false)
        {
            MoveStarted?.Invoke();
        }
    }

    public void StopMove()
    {
        if (_isEnabled == false)
        {
            return;
        }

        if (_target == null)
        {
            return;
        }

        if (_isStopped == false)
        {
            MoveEnded?.Invoke();
        }

        _agent.isStopped = true;
        _isStopped = true;
        _reachStoppingDistance = true;
    }

    private void FixedUpdate()
    {
        if (_isEnabled == false)
        {
            return;
        }

        if (_target == null)
        {
            return;
        }

        if (Vector3.Distance(_cachedTransform.position, _target.position) <= _stoppingDistance)
        {
            StopMove();
            _objectRotater.RotateToPoint(_target.position, Vector3.up, WorldAxis.Y);
        }
        else
        {
            _reachStoppingDistance = false;
        }
    }

    public void SetAiEnabled(bool state)
    {
        StopMove();
        _isEnabled = state;
    }
}