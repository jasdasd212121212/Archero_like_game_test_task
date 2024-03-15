using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Game design/Gun")]
public class GunSettingsScriptableObject : ScriptableObject
{
    [Header("Prefab")]

    [SerializeField] private ProjectileBase _projectilePrefab;

    [Header("Settings")]

    [SerializeField][Min(0.00001f)] private float _fireDellay = 0.1f;
    [SerializeField][Min(0.00001f)] private float _reloadTime = 2f;
    [SerializeField][Min(1)] private int _damage;
    [SerializeField][Min(2)] private int _maxAmmoInGun;

    public float FireDellay => _fireDellay;
    public float ReloadTime => _reloadTime;
    public int Damage => _damage;
    public int MaxAmmoInGun => _maxAmmoInGun;

    public ProjectileBase ProjectilePrefab => _projectilePrefab;
}