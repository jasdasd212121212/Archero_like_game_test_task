using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private CharacterMainGemeDesignSettingsScriptableObject _gameDesignSettings;

    private IInput _currentInputSystem;
    private CharacterController _characterController;

    public Vector2 CurrentInputVector => _currentInputSystem.GetInputVector();

    public event Action<bool> MoveStateChanged;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        FetchInputSystem();
    }

    private void FixedUpdate()
    {
        FetchInputSystem();
    }

    private void Update()
    {
        Vector3 input = new Vector3(_currentInputSystem.GetInputVector().normalized.x, 0, _currentInputSystem.GetInputVector().normalized.y);

        Vector3 nextPosition = input * _gameDesignSettings.PlayerMoveSpeed * Time.deltaTime;
        _characterController.Move(nextPosition);

        MoveStateChanged?.Invoke(input != Vector3.zero);
    }

    private void FetchInputSystem()
    {
        _currentInputSystem = InputSystemPhasade.CurrentInputSystem;
    }
}