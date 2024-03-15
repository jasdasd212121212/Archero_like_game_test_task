using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerHealthSystem _playerHealthSystem;

    [Space()]

    [SerializeField] private string _playerWalkName = "Walking";
    [SerializeField] private string _playerDeadAnimationTriggerName = "Die";

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _playerMover.MoveStateChanged += OnPlayerMove;
        _playerHealthSystem.PlayerDied += PlayDeadAnimation;
    }

    private void OnDestroy()
    {
        _playerMover.MoveStateChanged -= OnPlayerMove;
        _playerHealthSystem.PlayerDied -= PlayDeadAnimation;
    }

    private void OnPlayerMove(bool state)
    {
        _animator.SetBool(_playerWalkName, state);
    }

    private void PlayDeadAnimation()
    {
        _animator.SetTrigger(_playerDeadAnimationTriggerName);
    }
}