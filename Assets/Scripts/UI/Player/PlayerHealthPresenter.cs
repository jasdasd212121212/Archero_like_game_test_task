using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class PlayerHealthPresenter : MonoBehaviour
{
    [SerializeField] private Image _healthSlider;
    [SerializeField] private TextMeshProUGUI _healthText;

    private PlayerHealthSystem _healthSystem;

    [Inject]
    private void Construct(PlayerInstallationMark player)
    {
        _healthSystem = player.HealthSystem;
    }

    private void Start()
    {
        _healthSystem.HealthChanged += DisplayHealth;
        DisplayHealth();
    }

    private void OnDestroy()
    {
        _healthSystem.HealthChanged -= DisplayHealth;
    }

    private void DisplayHealth()
    {
        _healthSlider.fillAmount = (float)((float)Mathf.Clamp(_healthSystem.CurrentHealth, 1, int.MaxValue) / (float)_healthSystem.MaxHealth);
        _healthText.text = _healthSystem.CurrentHealth.ToString();
    }
}