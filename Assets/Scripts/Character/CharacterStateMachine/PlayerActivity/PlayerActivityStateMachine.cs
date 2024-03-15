using UnityEngine;

public class PlayerActivityStateMachine : MonoBehaviour
{
    [SerializeField] private PlayerMover _player;
    [SerializeField] private CharacterRotaterStateMachine _characterRotaterStateMachine;
    [SerializeField] private CharacterAutomaticlyShooter _charactedAutomaticlyShooter;

    private PlayerActiveState _activeState;
    private PlayerInactiveState _inactiveState;

    private StateMachine _stateMachine;

    private void Start()
    {
        _activeState = new PlayerActiveState(_characterRotaterStateMachine, _player, _charactedAutomaticlyShooter);
        _inactiveState = new PlayerInactiveState(_characterRotaterStateMachine, _player, _charactedAutomaticlyShooter);

        _stateMachine = new StateMachine(_activeState, new State[] { _activeState, _inactiveState }, false);
    }

    public void SetActive(bool state)
    {
        if (state == true)
        {
            _stateMachine.SetState(_activeState);
        }
        else
        {
            _stateMachine.SetState(_inactiveState);
        }
    }
}