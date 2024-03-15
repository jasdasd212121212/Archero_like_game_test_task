using UnityEngine;

public class GameObjectDellayedDeleter : MonoBehaviour
{
    [SerializeField][Min(0.001f)] private float _dellay;

    private void Start()
    {
        Invoke(nameof(DestroySelf), _dellay);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}