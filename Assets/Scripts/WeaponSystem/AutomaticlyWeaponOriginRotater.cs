using UnityEngine;

[RequireComponent(typeof(GameObjectRotater))]
public class AutomaticlyWeaponOriginRotater : MonoBehaviour
{
    private GameObjectRotater _rotater;
    private Transform _cachedTransform;

    private Transform _targetTransform;

    private Transform _cachedRotaterTransform;

    private void Awake()
    {
        _rotater = GetComponent<GameObjectRotater>();   

        _cachedTransform = transform;
        _cachedRotaterTransform = _rotater.transform;
    }

    public void SetTarget(Transform target)
    {
        _targetTransform = target;
    }

    private void FixedUpdate()
    {
        if (_targetTransform == null)
        {
            _cachedRotaterTransform.localRotation = Quaternion.Euler(0, 0, 0);
            return;
        }

        _rotater.RotateToPoint(_targetTransform.position, _cachedTransform.right, WorldAxis.X);
    }
}