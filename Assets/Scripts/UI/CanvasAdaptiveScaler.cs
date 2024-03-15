using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas)), RequireComponent(typeof(CanvasScaler))]
public class CanvasAdaptiveScaler : MonoBehaviour
{
    [SerializeField][Min(0)] private float _offset;

    private RectTransform _selfCanvas;
    private CanvasScaler _scaler;

    private void Start()
    {
        _selfCanvas = GetComponent<RectTransform>();
        _scaler = GetComponent<CanvasScaler>();
    }

    private void LateUpdate()
    {
        //if (_selfCanvas.rect.width < _selfCanvas.rect.height)
        //{
        //    _scaler.matchWidthOrHeight = 0f;
        //}
        //else
        //{

        //}

        //print(_selfCanvas.rect.width / _selfCanvas.rect.height);
        _scaler.matchWidthOrHeight = Mathf.Clamp01(Mathf.Clamp01(_selfCanvas.rect.width / _selfCanvas.rect.height) - _offset);
    }
}