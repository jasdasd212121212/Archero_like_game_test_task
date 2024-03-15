using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class CharacterMoveRotationHandler : MonoBehaviour
{
    [SerializeField] private GameObjectRotater _characterRotater;
    private PlayerMover _playerMover;

    private Transform _cachedTransform;

    private void Start()
    {
        _playerMover = GetComponent<PlayerMover>();

        _cachedTransform = _playerMover.transform;
    }

    private void OnEnable()
    {
        try
        {
            _playerMover.MoveStateChanged += OnPlayerMoveStateChanged;
        }
        catch { }
    }

    private void OnDisable()
    {
        _playerMover.MoveStateChanged -= OnPlayerMoveStateChanged;
    }

    private void OnPlayerMoveStateChanged(bool state)
    {
        if (state == false)
        {
            return;
        }

        Vector3 targetDirection = (new Vector3(_playerMover.CurrentInputVector.x, 0f, _playerMover.CurrentInputVector.y).normalized - _cachedTransform.forward.normalized) * Mathf.Pow(Time.deltaTime, 2);

        if (targetDirection != Vector3.zero)
        {
            _characterRotater.RotateToDirection(targetDirection, Vector3.up, WorldAxis.Y);
        }
    }
}