using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GunsCollector : MonoBehaviour
{
    [SerializeField] private GunChooser _chooser;

    private void OnTriggerEnter(Collider other)
    {
        GunDropItem gun = other.gameObject.GetComponent<GunDropItem>();

        if (gun != null)
        {
            _chooser.ChooseGunByIndex(gun.GunIndexInChooser);
            Destroy(gun.gameObject);
        }
    }
}