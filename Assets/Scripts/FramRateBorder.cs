using UnityEngine;

public class FramRateBorder : MonoBehaviour
{
    [SerializeField][Min(-1)] private int _targetFramRate = 60;

    private void Start()
    {
        Application.targetFrameRate = _targetFramRate;
    }
}