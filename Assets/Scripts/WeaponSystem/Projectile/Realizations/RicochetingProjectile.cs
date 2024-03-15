using UnityEngine;

public class RicochetingProjectile : ProjectileBase
{
    [SerializeField][Min(1)] private int _ricochetesMaxCount = 2;
    private int _ricochetesCount;

    private Collider _selfCollider;
    private Transform _cachedTransform;

    private void Awake()
    {
        _selfCollider = GetComponent<Collider>();
        _cachedTransform = transform;
    }

    protected override void OnPostCollisionEnter(bool isCollisionDamageble, Collision collision)
    {
        if (_ricochetesCount >= _ricochetesMaxCount)
        {
            Destroy(gameObject);
            return;
        }

        try
        {
            SelfRigidbody.velocity = Vector3.zero;
            SelfRigidbody.angularVelocity = Vector3.zero;

            _selfCollider.enabled = false;

            _cachedTransform.rotation = Quaternion.Euler(-_cachedTransform.eulerAngles);

            SelfRigidbody.AddForce(_cachedTransform.forward * InitialImpulse * Time.deltaTime, ForceMode.Impulse);

            _ricochetesCount++;

            Invoke(nameof(EnableCollider), Time.fixedDeltaTime);
        }
        catch 
        {
            Destroy(gameObject);
        }
    }

    private void EnableCollider()
    {
        _selfCollider.enabled = true;
    }
}