using UnityEngine;
using System;

public class PlayerInstallationMark : MonoBehaviour
{
    [SerializeField]private PlayerHitBox _hitBox;
    [SerializeField]private PlayerHealthSystem _healthSystem;
    [SerializeField]private PlayerActivityStateMachine _activityStateMachine;

    private Transform _cachedTransform;

    public Transform PlayerTransform 
    { 
        get
        {
            if (_cachedTransform == null)
            {
                InitTransform();
            }

            return _cachedTransform;
        }

        private set => throw new NotImplementedException();
    }

    public PlayerHitBox HitBox => _hitBox;
    public PlayerHealthSystem HealthSystem => _healthSystem;
    public PlayerActivityStateMachine ActivityStateMachine => _activityStateMachine;

    private void Awake()
    {
        if (_cachedTransform != null)
        {
            return;
        }

        InitTransform();
    }

    private void InitTransform()
    {
        _cachedTransform = transform;
    }
}