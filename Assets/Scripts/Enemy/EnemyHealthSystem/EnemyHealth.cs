using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyActivationStateMachine _selfStateMachine;
    [SerializeField] private HitBox _hitBox;
    [SerializeField] private EnemyMainGameDisignSettingsScriptableObject _settings;

    public event Action Died;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _settings.EnemyHealth;

        _hitBox.Damaged += OnDamaged;
    }

    private void OnDestroy()
    {
        _hitBox.Damaged -= OnDamaged;
    }

    private void OnDamaged(int damage)
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
            Die();
            _currentHealth = 0;
        }
    }

    private void Die()
    {
        _selfStateMachine.SetActive(false);
        _hitBox.gameObject.SetActive(false);

        Died?.Invoke();
    }
}