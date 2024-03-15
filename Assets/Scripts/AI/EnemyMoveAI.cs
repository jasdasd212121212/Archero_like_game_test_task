using UnityEngine;
using Zenject;

public class EnemyMoveAI : MonoBehaviour
{
    [SerializeField] private GameObject _selfMoverObject;
    [SerializeField][Min(0.2f)] private float _agreeDistance;
    [SerializeField] private EnemyMainGameDisignSettingsScriptableObject _enemyMainGameDisignSettingsScriptableObject;

    private Transform _target;
    private Transform _cachedTransform;
    private IAiControllable _selfMover;

    [Inject]
    private void Construct(PlayerInstallationMark player)
    {
        _target = player.PlayerTransform;
        _cachedTransform = transform;
    }

    private void OnValidate()
    {
        try
        {
            if (_selfMoverObject != null && _selfMoverObject.GetComponent<IAiControllable>() == null)
            {
                Debug.LogError($"Invalid object error -> gameObject with name <{_selfMoverObject.name}> are not have any scripts realises <{nameof(IAiControllable)}> interface");
                _selfMoverObject = null;
            }
        }
        catch { }
    }

    private void Start()
    {
        _selfMover = _selfMoverObject.GetComponent<IAiControllable>();

        _selfMover.Initialize(_target, _enemyMainGameDisignSettingsScriptableObject.EnemyMoveSpeed);
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(_cachedTransform.position, _target.position) <= _agreeDistance)
        {
            _selfMover.StartMove();
        }
        else
        {
            _selfMover.StopMove();
        }
    }
}