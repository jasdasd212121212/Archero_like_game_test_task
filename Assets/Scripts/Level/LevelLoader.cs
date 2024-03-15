using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private LevelProgressSaver _progressSaver;

    public void CompleateLevel()
    {
        _progressSaver.CompleateLevel();
        SceneManager.LoadScene(_progressSaver.CurrentLevelScneneIndex);
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(_progressSaver.CurrentLevelScneneIndex);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }
}