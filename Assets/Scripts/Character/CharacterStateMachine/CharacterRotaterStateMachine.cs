using UnityEngine;

public class CharacterRotaterStateMachine : MonoBehaviour
{
    [SerializeField] private CharacterMoveRotationHandler _characterMoveRotationHandler;
    [SerializeField] private PlayerMover _characterMover;
    [SerializeField] private CharacterAutomaticlyShooter _characterAutomaticallyShooter;

    private MoveState _characterMoveState;
    private EnemySearchState _characterEnemySearchState;

    private StateMachine _stateMachine;

    private void Awake()
    {
        _characterMoveState = new MoveState(_characterMoveRotationHandler, _characterAutomaticallyShooter);
        _characterEnemySearchState = new EnemySearchState(_characterMoveRotationHandler, _characterAutomaticallyShooter);

        _stateMachine = new StateMachine(_characterMoveState, new State[] { _characterMoveState, _characterEnemySearchState }, false);

        Invoke(nameof(StartActivate), Time.fixedDeltaTime);
    }

    private void OnEnable()
    {
        _characterMover.MoveStateChanged += OnMove;
    }

    private void OnDisable()
    {
        _characterMover.MoveStateChanged -= OnMove;
    }

    public void OnMove(bool state)
    {
        SetActive(state);
    }

    public void SetActive(bool state)
    {
        if (state == true)
        {
            _stateMachine.SetState(_characterMoveState);
        }
        else
        {
            _stateMachine.SetState(_characterEnemySearchState);
        }
    }

    private void StartActivate()
    {
        SetActive(true);
    }
}