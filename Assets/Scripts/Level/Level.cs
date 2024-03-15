using UnityEngine;
using System;
using Zenject;

public class Level : MonoBehaviour
{
    [SerializeField] private FinishTrigger _finishTrigger;

    [Space()]

    [SerializeField] private PauseStateMachine _pausedStateMachine;

    private PlayerHealthSystem _healthSystem;

    public event Action Wined;
    public event Action Losed;

    [Inject]
    private void Construct(PlayerInstallationMark player)
    {
        _healthSystem = player.HealthSystem;
    }

    private void Start()
    {
        _pausedStateMachine.SetPause(false);
    }

    private void Awake()
    {
        _healthSystem.PlayerDied += OnPlayerDied;
        _finishTrigger.Finished += OnPlayerFinish;
    }

    private void OnDestroy()
    {
        _healthSystem.PlayerDied -= OnPlayerDied;
        _finishTrigger.Finished -= OnPlayerFinish;
    }

    private void OnPlayerDied()
    {
        Losed?.Invoke();
        _pausedStateMachine.SetPause(true);
    }

    private void OnPlayerFinish()
    {
        Wined?.Invoke();
        _pausedStateMachine?.SetPause(true);
    }
}