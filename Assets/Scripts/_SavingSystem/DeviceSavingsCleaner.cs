using UnityEngine;

public class DeviceSavingsCleaner : MonoBehaviour
{
    private void Start()
    {
        string key = "FROMATTED";

        bool firstStart = !PlayerPrefs.HasKey(key);

        if (firstStart)
        {
            PlayerPrefs.DeleteAll();
        }

        PlayerPrefs.SetInt(key, 1);
    }
}