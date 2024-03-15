using UnityEngine;

[RequireComponent(typeof(Gun))]
public class GunAnimator : MonoBehaviour
{
    [SerializeField] private string _gunReloadAnimationTriggerName = "Reloading";
    [SerializeField] private Animator _animator;

    private Gun _gun;

    private void Awake()
    {
        _gun = GetComponent<Gun>();

        _gun.ReloadingStarted += PlayReloadingAnimation;
    }

    private void OnDestroy()
    {
        _gun.ReloadingStarted -= PlayReloadingAnimation;
    }

    private void PlayReloadingAnimation()
    {
        _animator.SetTrigger(_gunReloadAnimationTriggerName);
    }
}