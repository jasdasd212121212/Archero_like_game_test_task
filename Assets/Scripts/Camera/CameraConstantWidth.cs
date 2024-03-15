using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraConstantWidth : MonoBehaviour
{
    [SerializeField] private Vector2 DefaultResolution = new Vector2(1920, 1080);
    [Range(0f, 1f)] public float WidthOrHeight = 0;

    private Camera _componentCamera;

    private float _targetAspect;

    private float _initialFov;
    private float _horizontalFov = 120f;

    private void Start()
    {
        _componentCamera = GetComponent<Camera>();

        _targetAspect = DefaultResolution.x / DefaultResolution.y;

        _initialFov = _componentCamera.fieldOfView;
        _horizontalFov = CalcVerticalFov(_initialFov, 1 / _targetAspect);
    }

    private void Update()
    {
        float constantWidthFov = CalcVerticalFov(_horizontalFov, _componentCamera.aspect);
        _componentCamera.fieldOfView = Mathf.Lerp(constantWidthFov, _initialFov, WidthOrHeight);
    }

    private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
    {
        float horizontalFovInRads = hFovInDeg * Mathf.Deg2Rad;

        float verticalFovInRads = 2 * Mathf.Atan(Mathf.Tan(horizontalFovInRads / 2) / aspectRatio);

        return verticalFovInRads * Mathf.Rad2Deg;
    }
}