using UnityEngine;

public class JSONPlayerPrefsSerializer : ISavingService
{
    public T Load<T>(string key)
    {
        string json = PlayerPrefs.GetString(key);
        T loaded = JsonUtility.FromJson<T>(json);

        Debug.Log(json);

        return loaded;
    }

    public void Save<T>(string key, T value)
    {
        string json = JsonUtility.ToJson(value);
        PlayerPrefs.SetString(key, json);

        Debug.Log(json);
    }
}