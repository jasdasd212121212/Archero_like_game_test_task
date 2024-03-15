using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public abstract class ProjectileBase : MonoBehaviour
{
    [SerializeField][Min(0.01f)] private float _initialImpulse;

    private int _damage;
    private bool _initialized = false;

    private Rigidbody _selfRigidbody;
    protected Rigidbody SelfRigidbody => _selfRigidbody;
    protected float InitialImpulse => _initialImpulse;

    public event Action CollidedWithObject;

    public void Initialize(int damage)
    {
        if (_initialized == true)
        {
            return;
        }

        if (damage <= 0)
        {
            Debug.LogError($"Invalid damage error -> damage = {damage}");
            damage = Mathf.Clamp(damage, 1, int.MaxValue);
        }

        _damage = damage;

        _initialized = true;
    }

    private void Start()
    {
        _selfRigidbody = GetComponent<Rigidbody>();
        _selfRigidbody.AddForce(transform.forward * _initialImpulse * Time.deltaTime, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageble damageble = collision.gameObject.GetComponentInChildren<IDamageble>();
        bool isDamageble = damageble != null;

        if (isDamageble == true)
        {
            damageble.TakeDamage(_damage);
        }

        CollidedWithObject?.Invoke();
        OnPostCollisionEnter(isDamageble, collision);
    }

    protected virtual void OnPostCollisionEnter(bool isCollisionDamageble, Collision collision)
    {
        Destroy(gameObject);
    }
}