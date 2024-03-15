using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private GameObject _moverGameObject;
    [SerializeField] private EnemyHealth _healthSystem;

    [Space()]

    [SerializeField] private string _moveAnimationName = "Walking";
    [SerializeField] private string _deadAnimationName = "Dead";

    private Animator _animator;
    private IAiObservable _mover;

    private void OnValidate()
    {
        try
        {
            if (_moverGameObject.GetComponent<IAiObservable>() == null && _moverGameObject != null)
            {
                Debug.LogError($"Invalid game object by name {_moverGameObject.name} -> haven`t scripts are realises {nameof(IAiObservable)} interface");
                _moverGameObject = null;
            }
        }
        catch { }
    }

    private void Awake()
    {
        _mover = _moverGameObject.GetComponent<IAiObservable>();

        _animator = GetComponent<Animator>();

        _mover.MoveStarted += StartWalkAnimation;
        _mover.MoveEnded += StopWalkAnimation;

        _healthSystem.Died += PlayDeadAnimation;
    }

    private void OnDestroy()
    {
        _mover.MoveStarted -= StartWalkAnimation;
        _mover.MoveEnded -= StopWalkAnimation;

        _healthSystem.Died -= PlayDeadAnimation;
    }

    private void StartWalkAnimation() 
    {
        _animator.SetBool(_moveAnimationName, true);
    }

    private void StopWalkAnimation()
    {
        _animator.SetBool(_moveAnimationName, false);
    }

    private void PlayDeadAnimation()
    {
        _animator.SetTrigger(_deadAnimationName);
    }
}