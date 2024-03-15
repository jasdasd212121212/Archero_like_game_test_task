using UnityEngine;

[RequireComponent(typeof(Gun))]
public class GunSoundsPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _fireSound;
    [SerializeField] private AudioSource _reloadSound;

    private Gun _gun;

    private void Awake()
    {
        _gun = GetComponent<Gun>();

        _gun.Shooted += OnShoot;
        _gun.ReloadingStarted += OnReload;
    }

    private void OnDestroy()
    {
        _gun.Shooted -= OnShoot;
        _gun.ReloadingStarted -= OnReload;
    }

    private void OnShoot()
    {
        _fireSound.Play();
    }

    private void OnReload()
    {
        _reloadSound.Play();
    }
}