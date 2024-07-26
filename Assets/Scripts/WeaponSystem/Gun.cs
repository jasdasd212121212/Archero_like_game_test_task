using UnityEngine;
using System;
using System.Collections;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _projectileOrigin;
    [SerializeField] private GunSettingsScriptableObject _gunSettings;

    private int _currentAmmoInGun;
    private float _nextShotTime;

    private bool _isReloading;
    private bool _isShooting;

    private MonoFactory<ProjectileBase> _projectileFactory;

    public int CurrentAmmoInGun => _currentAmmoInGun;

    public event Action Shooted;
    public event Action ReloadingStarted;

    private void Start()
    {
        _currentAmmoInGun = _gunSettings.MaxAmmoInGun;
        _projectileFactory = new MonoFactory<ProjectileBase>(_gunSettings.ProjectilePrefab);
    }

    public void SetShoot(bool state)
    {
        _isShooting = state;
    }

    private void FixedUpdate()
    {
        if (Time.time > _nextShotTime && _isShooting == true && _isReloading == false)
        {
            Shot();
            _nextShotTime = Time.time + _gunSettings.FireDellay;
        }
    }

    private void Shot()
    {
        if (_currentAmmoInGun <= 0)
        {
            if (_isReloading == false)
            {
                StartCoroutine(nameof(Reload));
                _isReloading = true;
            }

            return;
        }

        _projectileFactory.CreateWithoutParent(_projectileOrigin.position, Quaternion.Euler(_projectileOrigin.eulerAngles)).Initialize(_gunSettings.Damage);

        _currentAmmoInGun--;
        Shooted?.Invoke();
    }

    private IEnumerator Reload()
    {
        ReloadingStarted?.Invoke();

        _isReloading = true;

        yield return new WaitForSeconds(_gunSettings.ReloadTime);

        _currentAmmoInGun = _gunSettings.MaxAmmoInGun;
        _isReloading = false;
    }
}