using UnityEngine;
using Zenject;

public class CameraPlayerFollow : MonoBehaviour
{
    [SerializeField] private WorldAxis _followingAxis;
    [SerializeField] private Vector3 _offset;
    [SerializeField][Min(0.1f)] private float _lerpSpeed;

    private Transform _cachedTransfrom;
    private Vector3 _inititalPosition;

    [Inject] private PlayerInstallationMark _targetTransform;

    private void Start()
    {
        _cachedTransfrom = transform;
        _inititalPosition = _cachedTransfrom.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPositionRaw = Vector3.zero;

        switch (_followingAxis) 
        {
            case WorldAxis.X:
                    targetPositionRaw = new Vector3(_targetTransform.PlayerTransform.position.x, _inititalPosition.y, _inititalPosition.z);
                break;
            case WorldAxis.Y:
                    targetPositionRaw = new Vector3(_inititalPosition.x, _targetTransform.PlayerTransform.position.y, _inititalPosition.z);
                break;
            case WorldAxis.Z:
                    targetPositionRaw = new Vector3(_inititalPosition.x, _inititalPosition.y, _targetTransform.PlayerTransform.position.z);
                break;
        }

        Vector3 targetPosition = new Vector3(targetPositionRaw.x + _offset.x, targetPositionRaw.y + _offset.y, targetPositionRaw.z + _offset.z);

        _cachedTransfrom.position = Vector3.Slerp(_cachedTransfrom.position, targetPosition, _lerpSpeed * Time.deltaTime);
    }
}