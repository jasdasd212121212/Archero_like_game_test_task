using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class AiFlyMover : MonoBehaviour, IActiveAIControllable, IAiObservable, IAiControllable
{
    [SerializeField] private float _flightHeights = 6f;
    [SerializeField][Min(0)] private float _stoppingDistance;
    [SerializeField] private GameObjectRotater _objectRotater;

    private Transform _target;
    private Transform _cachedTransform;
    private Rigidbody _cachedRigidbody;

    private float _moveSpeed;
    private bool _isEnabled;
    private bool _isStopped;
    private bool _isReachedStoppingDistance;

    public float FlyingHeights => _flightHeights;

    public event Action MoveStarted;
    public event Action MoveEnded;

    private void Awake()
    {
        _isEnabled = true;

        _cachedTransform = transform;
        _cachedRigidbody = GetComponent<Rigidbody>();

        _cachedTransform.position = new Vector3(_cachedTransform.position.x, _flightHeights, _cachedTransform.position.z);

        _cachedRigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _cachedRigidbody.mass = 100000f;
    }

    public void Initialize(Transform target, float moveSpeed)
    {
        _target = target;
        _moveSpeed = moveSpeed;
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

        if (_isReachedStoppingDistance == true)
        {
            return;
        }

        _isStopped = false;

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

        _isStopped = true;
        _isReachedStoppingDistance = true;
    }

    private void FixedUpdate()
    {
        if (_isEnabled == false)
        {
            return;
        }

        _objectRotater.RotateToPoint(_target.position, Vector3.up, WorldAxis.Y);

        if (Vector3.Distance(_cachedTransform.position, _target.position) >= _stoppingDistance && _isStopped == false)
        {
            MoveToPlayer();
            _isReachedStoppingDistance = true;
        }
        else
        {
            _isReachedStoppingDistance = false;
        }
    }

    private void MoveToPlayer()
    {
        Vector3 selfProjectedPosition = new Vector3(_cachedTransform.position.x, _flightHeights, _cachedTransform.position.z);
        Vector3 targetProjectedPosition = new Vector3(_target.position.x, _flightHeights, _target.position.z);

        _cachedRigidbody.MovePosition(Vector3.MoveTowards(selfProjectedPosition, targetProjectedPosition, _moveSpeed * Time.fixedDeltaTime));
    }

    public void SetAiEnabled(bool state)
    {
        _isEnabled = state;
    }
}