using UnityEngine;
using Zenject;

public class EnemyAutomaticlyShooter : MonoBehaviour
{
    [SerializeField] private float _shootingDistance = 15f;
    [SerializeField] private AutomaticlyWeaponOriginRotater _weaponOriginRotater;
    [SerializeField] private Gun _enemyGun;

    private Transform _player;
    private Transform _cachedTransform;

    [Inject]
    private void Construct(PlayerInstallationMark player)
    {
        _player = player.PlayerTransform;
    }

    private void Start()
    {
        _cachedTransform = transform;
    }

    private void OnDisable()
    {
        DisableGun();
    }

    private void FixedUpdate()
    {
        if (enabled == false)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(_cachedTransform.position, _player.position);

        if (distanceToPlayer <= _shootingDistance)
        {
            _weaponOriginRotater.SetTarget(_player);
            _enemyGun.SetShoot(true);
        }
        else
        {
            DisableGun();
        }
    }

    private void DisableGun()
    {
        _weaponOriginRotater.SetTarget(null);
        _enemyGun.SetShoot(false);
    }
}