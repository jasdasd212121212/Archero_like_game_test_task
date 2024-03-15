using UnityEngine;

public class EnemyMarc : MonoBehaviour
{
    [SerializeField] private EnemyHealth _health;

    private void Awake()
    {
        _health.Died += DestroySelf;
    }

    private void OnDestroy()
    {
        _health.Died -= DestroySelf;
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}