using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth), typeof(AiFlyMover), typeof(Rigidbody))]
public class DeadDestructionHandler : MonoBehaviour
{
    [SerializeField] private Transform _checkOrigin;
    [SerializeField] private LayerMask _groundCheckMask;
    [SerializeField][Min(0.001f)] private float _lerpSpeed = 10f;
    [SerializeField][Min(0.001f)] private float _stopLerpingDistance = 0.5f;

    private Vector3 _groundPoint;
    private Transform _cachedTransform;
    private Rigidbody _cachedRigidbody;

    private AiFlyMover _mover;
    private EnemyHealth _enemyHealth;

    private bool _isUsed;

    private void Start()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _mover = GetComponent<AiFlyMover>();
        _cachedRigidbody = GetComponent<Rigidbody>();

        _cachedTransform = transform;

        _isUsed = false;

        _enemyHealth.Died += MoveToGroundPose;
    }

    private void OnDestroy()
    {
        _enemyHealth.Died -= MoveToGroundPose;
    }

    private void MoveToGroundPose()
    {
        if (_isUsed == true)
        {
            return;
        }

        RaycastHit hit;
        _cachedRigidbody.isKinematic = true;

        if (Physics.Raycast(_cachedTransform.position, Vector3.down, out hit, _mover.FlyingHeights, _groundCheckMask))
        {
            _groundPoint = hit.point;
            StartCoroutine(nameof(LerpToGroundPostion));
        }

        _isUsed = false;
    }

    private IEnumerator LerpToGroundPostion()
    {
        while (Vector3.Distance(_cachedTransform.position, _groundPoint) >= _stopLerpingDistance)
        {
            _cachedTransform.position = Vector3.Lerp(_cachedTransform.position, _groundPoint, _lerpSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}