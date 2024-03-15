using UnityEngine;

[RequireComponent(typeof(Level))]
public class LevelScreen : MonoBehaviour
{
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _winPanel;

    private Level _level;

    private void Awake()
    {
        _level = GetComponent<Level>();

        _level.Losed += EnableLosePanel;
        _level.Wined += EnableWinPanel;

        DisableAll();
    }

    private void OnDestroy()
    {
        _level.Losed -= EnableLosePanel;
        _level.Wined -= EnableWinPanel;
    }

    private void EnableLosePanel()
    {
        DisableAll();
        _losePanel.SetActive(true);
    }

    private void EnableWinPanel()
    {
        DisableAll();
        _winPanel.SetActive(true);
    }

    private void DisableAll()
    {
        _losePanel.SetActive(false);
        _winPanel.SetActive(false);
    }
}