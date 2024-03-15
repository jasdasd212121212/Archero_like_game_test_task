using UnityEngine;

public class StateMachine
{
    private State _currentState;
    private State[] _states;

    private bool _logingNotCriticalParameters;

    public StateMachine(State initialState, State[] states)
    {
        _currentState = initialState;
        _currentState.Entry();

        _states = states;

        if (_states.Length == 0 || _states == null)
        {
            _states = new State[1];
            _states[0] = initialState;

            Debug.LogWarning("Warning satate machine >>> have only 1 state");
        }

        _logingNotCriticalParameters = true;
    }

    public StateMachine(State initialState, State[] states, bool logingNotCritical) : this(initialState, states)
    {
        _logingNotCriticalParameters = logingNotCritical;
    }

    public void SetState(State newState)
    {
        if (newState == _currentState)
        {
            if (_logingNotCriticalParameters == true)
            {
                Debug.Log("Can`t set idential states");
            }

            return;
        }

        if (Contains(newState) == false)
        {
            Debug.LogError($"Can`t set state because new state are not contains in array");
            return;
        }

        _currentState.Exit();
        _currentState = newState;
        _currentState.Entry();
    }

    private bool Contains(State state)
    {
        for (int i = 0; i < _states.Length; i++)
        {
            if (_states[i] == state)
            {
                return true;
            }
        }

        return false;
    }
}