using UnityEngine;
using Zenject;
using System;

public class GameStateMachine : MonoBehaviour
{
    private PlayerActivityStateMachine _player;

    public event Action GameContinued;

    [Inject]
    private void Construct(PlayerInstallationMark player)
    {
        _player = player.ActivityStateMachine;
    }

    public void SetActive(bool state)
    {
        _player.SetActive(state);

        if (state == true)
        {
            GameContinued?.Invoke();
        }
    }
}