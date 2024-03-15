using UnityEngine;

public class LevelProgressSaver : MonoBehaviour
{
    private int _currentLevel;

    public int CurrentLevel => _currentLevel;
    public int CurrentLevelScneneIndex => _currentLevel + BUILD_INDEX_OFFSET;

    private const int BUILD_INDEX_OFFSET = 1;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(SavingSystemConfig.CURRENT_LEVEL_SAVEKEY) == false)
        {
            _currentLevel = 0;
            Save();
        }
        else
        {
            _currentLevel = PlayerPrefs.GetInt(SavingSystemConfig.CURRENT_LEVEL_SAVEKEY);
        }
    }

    public void CompleateLevel()
    {
        _currentLevel++;
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetInt(SavingSystemConfig.CURRENT_LEVEL_SAVEKEY, _currentLevel);
    }
}