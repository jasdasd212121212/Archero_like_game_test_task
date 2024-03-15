using UnityEngine;

public class EnemyHealthGunActivityHandler : MonoBehaviour
{
    [SerializeField] private EnemyHealth _healthSystem;
    [SerializeField] private EnemyAutomaticlyShooter _shooter;

    private void Awake()
    {
        _healthSystem.Died += DisbaleShooter;
    }

    private void OnDestroy()
    {
        _healthSystem.Died -= DisbaleShooter;
    }

    private void DisbaleShooter()
    {
        _shooter.enabled = false;
    }
}