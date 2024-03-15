using UnityEngine;
using System;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private CharacterMainGemeDesignSettingsScriptableObject _playerSettings;
    [SerializeField]private PlayerActivityStateMachine _playerActivityStateMachine;

    [Space()]

    [SerializeField] private PlayerHitBox _hitBox;

    private int _currentHealth;
    private bool _isDead = false;

    public int MaxHealth => _playerSettings.PlayerHealth;
    public int CurrentHealth => _currentHealth;   

    public event Action HealthChanged;
    public event Action PlayerDied;

    private void Start()
    {
        _currentHealth = _playerSettings.PlayerHealth;
        _isDead = false;

        _hitBox.Damaged += TakeDamage;
    }

    private void OnDestroy()
    {
        _hitBox.Damaged -= TakeDamage;
    }

    private void TakeDamage(int damage)
    {
        if (_currentHealth <= 0)
        {
            return;
        }

        if (damage < 0)
        {
            Debug.LogError("Can`t set damage < 0");
            damage = 1;
        }

        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }

        HealthChanged?.Invoke();
    }

    private void Die()
    {
        if (_isDead == true)
        {
            return;
        }

        _playerActivityStateMachine.SetActive(false);
        _isDead = true;

        PlayerDied?.Invoke();
    }
}