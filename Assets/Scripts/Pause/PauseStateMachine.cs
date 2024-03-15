using UnityEngine;

public class PauseStateMachine : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

    private StateMachine _stateMachine;

    private PausedState _pausedState;
    private UnpausedState _unpausedState;

    private void Awake()
    {
        _pausedState = new PausedState();
        _unpausedState = new UnpausedState();

        _stateMachine = new StateMachine(_unpausedState, new State[] { _pausedState, _unpausedState }, false);
    }

    public void SetPauseWithPanel(bool state)
    {
        SetPause(state);
        _pausePanel.SetActive(state);
    }

    public void SetPause(bool state)
    {
        if (state == true)
        {
            _stateMachine.SetState(_pausedState);
        }
        else
        {
            _stateMachine.SetState(_unpausedState);
        }
    }
}