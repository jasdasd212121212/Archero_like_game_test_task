using System.Collections;
using UnityEngine;
using Zenject;
using System;

public class EnemyExploder : MonoBehaviour
{
    [SerializeField]private EnemyExploderSettingsScriptableObject _settings;
    [SerializeField] private EnemyHealth _selfHealth;

    [Space()]

    [SerializeField] private EnemyActivationStateMachine _enemyStateMachine;
    [SerializeField] private GameObject _enemyRoot;

    private Transform _player;
    private Transform _cachedTransform;
    private PlayerHitBox _healthSystem;

    private WaitForSeconds _waitForUpdate;

    private bool _detected;
    private bool _canExplode;

    private const float UPDATE_DELLAY = 0.001f;

    public event Action Exploded;
    public event Action ExplodeStarted;

    [Inject]
    private void Construct(PlayerInstallationMark player)
    {
        _player = player.PlayerTransform;
        _healthSystem = player.HitBox;
    }

    private void Start()
    {
        _cachedTransform = transform;

        _waitForUpdate = new WaitForSeconds(UPDATE_DELLAY);

        _selfHealth.Died += () => { _canExplode = false; };
        _canExplode = true;

        StartCoroutine(nameof(DetectionLoop));
    }

    private IEnumerator DetectionLoop()
    {
        while (_detected == false && _canExplode == true)
        {
            if (_canExplode == false)
            {
                yield break;
            }

            if (Vector3.Distance(_cachedTransform.position, _player.position) <= _settings.ExplodeDistance && _canExplode == true)
            {
                _enemyStateMachine.SetActive(false);
                ExplodeStarted?.Invoke();

                Invoke(nameof(Explode), _settings.ExplodeDellay);

                _detected = true;

                StopCoroutine(nameof(DetectionLoop));
                yield break;
            }

            yield return _waitForUpdate;
        }
    }

    private void Explode()
    {
        Exploded?.Invoke();
        Destroy(_enemyRoot);

        if (Vector3.Distance(_cachedTransform.position, _player.position) <= _settings.ExplodeDistance)
        {
            _healthSystem.TakeDamage(_settings.ExplodeDamage);
        }
    }
}