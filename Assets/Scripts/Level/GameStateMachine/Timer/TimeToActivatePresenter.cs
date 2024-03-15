using TMPro;
using UnityEngine;

[RequireComponent(typeof(GameActivationTimer))]
public class TimeToActivatePresenter : MonoBehaviour
{
    [SerializeField] private GameStateMachine _gameStateMachine;

    [Space()]

    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private GameObject _waitPanelPlaceholder;
    [SerializeField] private GameObject _gameGuiPanel;

    private GameActivationTimer _gameActivationTimer;

    private void Start()
    {
        _gameActivationTimer = GetComponent<GameActivationTimer>();

        _gameActivationTimer.TimeChanged += PresetnTime;
        _gameStateMachine.GameContinued += DisableWaitPanel;

        EnableWaitPanel();

        PresetnTime();
    }

    private void OnDestroy()
    {
        _gameActivationTimer.TimeChanged -= PresetnTime;
        _gameStateMachine.GameContinued -= DisableWaitPanel;
    }

    private void PresetnTime()
    {
        _timeText.text = _gameActivationTimer.CurrentTime.ToString();
    }

    private void DisableWaitPanel()
    {
        _waitPanelPlaceholder.SetActive(false);
        _gameGuiPanel.SetActive(true);
    }

    private void EnableWaitPanel()
    {
        _waitPanelPlaceholder.SetActive(true);
        _gameGuiPanel.SetActive(false);
    }
}