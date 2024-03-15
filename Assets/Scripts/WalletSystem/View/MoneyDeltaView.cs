using UnityEngine;
using TMPro;

public class MoneyDeltaView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _counterText;

    private void Start()
    {
        Wallet.MoneyChanged += UpdateUI;
        UpdateUI();
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    private void OnDestroy()
    {
        Wallet.MoneyChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        string suffix = "";

        if (Wallet.LastDelta > 0)
        {
            suffix = "+";
        }

        _counterText.text = suffix + Wallet.LastDelta.ToString();
    }
}