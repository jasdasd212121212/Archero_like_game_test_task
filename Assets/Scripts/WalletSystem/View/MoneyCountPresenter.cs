using UnityEngine;
using TMPro;

public class MoneyCountPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _counterText;

    private void Start()
    {
        Wallet.MoneyChanged += UpdateUI;  
        UpdateUI();
    }

    private void OnDestroy()
    {
        Wallet.MoneyChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        _counterText.text = Wallet.money.ToString();
    }
}