using UnityEngine;

public class GameObjectRotater : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField][Min(0.01f)] private float _rotationSpeed = 10f;

    public void RotateToPoint(Vector3 point, Vector3 axis, WorldAxis accelerationAxis)
    {
        Vector3 directionToPoint = point - _targetTransform.position;
        RotateToDirection(directionToPoint, axis, accelerationAxis);
    }

    public void RotateToDirection(Vector3 direction, Vector3 axis, WorldAxis accelerationAxis)
    {
        Vector3 rotationBefore = _targetTransform.eulerAngles;
        _targetTransform.rotation = Quaternion.RotateTowards(_targetTransform.rotation, Quaternion.LookRotation(direction, axis), _rotationSpeed * Time.deltaTime);

        switch (accelerationAxis)
        {
            case WorldAxis.X:
                _targetTransform.rotation = Quaternion.Euler(_targetTransform.eulerAngles.x, rotationBefore.y, rotationBefore.z);
                break; 
            case WorldAxis.Y:
                _targetTransform.rotation = Quaternion.Euler(rotationBefore.x, _targetTransform.eulerAngles.y, rotationBefore.z);
                break;
            case WorldAxis.Z:
                _targetTransform.rotation = Quaternion.Euler(rotationBefore.x, rotationBefore.y, _targetTransform.eulerAngles.z);
                break;
        }
    }
}