using UnityEngine;
using System;
using System.Collections;

public class GameActivationTimer : MonoBehaviour
{
    [SerializeField][Min(0)] private int _activationDelay = 3;
    [SerializeField]private GameStateMachine _gameStateMachine;

    private int _currentTime;
    public int CurrentTime => _currentTime;

    public event Action TimeChanged;

    private void Awake()
    {
        _currentTime = _activationDelay;
    }

    private void Start()
    {
        _gameStateMachine.SetActive(false);
        StartCoroutine(nameof(GameTimer));
    }

    private IEnumerator GameTimer()
    {
        for (int i = 0; i < _activationDelay; i++)
        {
            yield return new WaitForSeconds(1);
            _currentTime--;
            TimeChanged?.Invoke();
        }

        _gameStateMachine.SetActive(true);
    }
}