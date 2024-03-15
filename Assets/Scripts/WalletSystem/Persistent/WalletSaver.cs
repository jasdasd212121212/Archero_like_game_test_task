using UnityEngine;

public class WalletSaver : ISavingService
{
    private int _startMoney;

    public WalletSaver(int startMoney)
    {
        _startMoney = startMoney;
    }

    public T Load<T>(string key)
    {
        string fromaterKey = "FROMATTED";

        bool firstStart = !PlayerPrefs.HasKey(fromaterKey);

        if (firstStart)
        {
            PlayerPrefs.DeleteKey(key);
        }

        PlayerPrefs.SetInt(fromaterKey, 1);

        if (PlayerPrefs.HasKey(key) == false)
        {
            Save<int>(key, _startMoney);
            return (T)(object)_startMoney;
        }

        int loaded = PlayerPrefs.GetInt(key);
        return (T)(object)loaded;
    } 

    public void Save<T>(string key, T value)
    {
        if (typeof(T) != typeof(int))
        {
            Debug.LogError("Can`t save not int param!");
        }

        PlayerPrefs.SetInt(key, (int)(object)value);
    }
}