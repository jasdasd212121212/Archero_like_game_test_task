using UnityEngine;

[RequireComponent(typeof(EnemyExploder))]
public class EnemyExploderSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _explodeSound;
    [SerializeField] private AudioSource _explodeStartedSound;

    private EnemyExploder _enemyExploder;

    private void Start()
    {
        _enemyExploder = GetComponent<EnemyExploder>();

        _enemyExploder.Exploded += PlayExplodeSound;
        _enemyExploder.ExplodeStarted += PlayExplodeStartedSound;
    }

    private void OnDestroy()
    {
        _enemyExploder.Exploded -= PlayExplodeSound;
        _enemyExploder.ExplodeStarted -= PlayExplodeStartedSound;
    }

    private void PlayExplodeSound()
    {
        _explodeSound.transform.SetParent(null);
        _explodeSound.Play();
    }

    private void PlayExplodeStartedSound()
    {
        _explodeStartedSound.Play();
    }
}