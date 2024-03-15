using UnityEngine;

public class EnemyActivationStateMachine : MonoBehaviour
{
    [SerializeField] private GameObject _aiActvationControllableObject;

    private IActiveAIControllable _aiActvationControllable;

    private EnemyActiveState _enemyActiveState;
    private EnemyInactiveState _enemyInactiveState;

    private StateMachine _stateMachine;

    private void OnValidate()
    {
        try
        {
            if (_aiActvationControllableObject != null && _aiActvationControllableObject.GetComponent<IActiveAIControllable>() == null)
            {
                Debug.LogError($"Game object with name <{_aiActvationControllableObject.name}> not contains any script realises <{nameof(IActiveAIControllable)}> interface");
                _aiActvationControllableObject = null;
            }
        }
        catch { }
    }

    private void Start()
    {
        _aiActvationControllable = _aiActvationControllableObject.GetComponent<IActiveAIControllable>();

        _enemyActiveState = new EnemyActiveState(_aiActvationControllable);
        _enemyInactiveState = new EnemyInactiveState(_aiActvationControllable);

        _stateMachine = new StateMachine(_enemyActiveState, new State[] { _enemyActiveState, _enemyInactiveState }, false);
    }

    public void SetActive(bool state)
    {
        if (state == true)
        {
            _stateMachine.SetState(_enemyActiveState);
        }
        else
        {
            _stateMachine.SetState(_enemyInactiveState);
        }
    }
}