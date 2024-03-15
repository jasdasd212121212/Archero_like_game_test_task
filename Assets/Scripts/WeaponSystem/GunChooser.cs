using UnityEngine;

public class GunChooser : MonoBehaviour
{
    [SerializeField][Min(0)] private int _startGunIndex = 0;
    [SerializeField] private Gun[] _guns;

    private Gun _currentGun;
    public Gun CurrentGun => _currentGun;

    private void OnValidate()
    {
        _startGunIndex = Mathf.Clamp(_startGunIndex, 0, (_guns.Length - 1));
    }

    private void Start()
    {
        ChooseGunByIndex(_startGunIndex);
    }

    public void ChooseGunByIndex(int index)
    {
        if (index < 0 || index > (_guns.Length - 1))
        {
            Debug.LogError("Choosing gun rejected -> invalid index out of bounds");
            return;
        }

        DisableAll();

        _currentGun = _guns[index];

        _currentGun.gameObject.SetActive(true);
    }

    private void DisableAll()
    {
        for (int i = 0; i < _guns.Length; i++)
        {
            _guns[i].gameObject.SetActive(false);
        }
    }
}