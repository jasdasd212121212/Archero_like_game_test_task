using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GunDropItem : MonoBehaviour
{
    [SerializeField][Min(0)] private int _gunIndexInChooser = 0;

    public int GunIndexInChooser => _gunIndexInChooser;
}