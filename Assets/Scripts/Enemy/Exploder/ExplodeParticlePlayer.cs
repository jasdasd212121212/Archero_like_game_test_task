using UnityEngine;

[RequireComponent(typeof(EnemyExploder))]
public class ExplodeParticlePlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystePrefab;

    private EnemyExploder _enemyExploder;
    private Transform _cachedTransform;

    private void Start()
    {
        _enemyExploder = GetComponent<EnemyExploder>();
        _cachedTransform = transform;

        _enemyExploder.Exploded += SpawnParticlesEffect;
    }

    private void OnDestroy()
    {
        _enemyExploder.Exploded -= SpawnParticlesEffect;
    }

    private void SpawnParticlesEffect()
    {
        Instantiate(_particleSystePrefab, _cachedTransform.position, Quaternion.identity);
    }
}