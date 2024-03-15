using System.Collections;
using UnityEngine;

public class CharacterAutomaticlyShooter : MonoBehaviour
{
    [SerializeField] private AutomaticlyWeaponOriginRotater _weaponRotater;
    [SerializeField] private GameObjectRotater _playerRotater;
    [SerializeField] private GunChooser _gunChooser;

    [Space()]

    [SerializeField][Min(0.1f)] private float _shootingDistance = 10f;

    private Transform[] _enemyes;
    private WaitForSeconds _waitForShootingUpdate;

    private Transform _cachedTransform;
    private bool _initialized;

    private const float UPDATE_DELLAY = 0.001f;

    private void Start()
    {
        EnemyMarc[] enemyes = FindObjectsOfType<EnemyMarc>();

        _enemyes = new Transform[enemyes.Length];

        for (int i = 0; i < _enemyes.Length; i++)
        {
            _enemyes[i] = enemyes[i].transform;
        }

        _initialized = true;
    }

    private void Awake()
    {
        _waitForShootingUpdate = new WaitForSeconds(UPDATE_DELLAY);
        _cachedTransform = transform;
    }

    private void OnEnable()
    {
        if (_initialized == false)
        {
            return;
        }

        StartCoroutine(nameof(ShootingLoop));
    }

    private void OnDisable()
    {
        try
        {
            StopCoroutine(nameof(ShootingLoop));
            _gunChooser.CurrentGun.SetShoot(false);

            _weaponRotater.SetTarget(null);
        }
        catch { }
    }

    private IEnumerator ShootingLoop()
    {
        if (_initialized == false)
        {
            yield break;
        }

        while (enabled == true) 
        {
            Transform currentEnemy = GetNearestEnemy();

            if (currentEnemy != null)
            {
                if (Vector3.Distance(_cachedTransform.position, currentEnemy.position) > _shootingDistance)
                {
                    _gunChooser.CurrentGun.SetShoot(false);
                    yield return _waitForShootingUpdate;
                    continue;
                }
            }

            _weaponRotater.SetTarget(currentEnemy);

            if (currentEnemy == null)
            {
                _gunChooser.CurrentGun.SetShoot(false);

                yield return _waitForShootingUpdate;
                continue;
            }

            if (Vector3.Distance(_cachedTransform.position, currentEnemy.position) > _shootingDistance)
            {
                _weaponRotater.SetTarget(null);
                yield return _waitForShootingUpdate;
                continue;
            }

            _gunChooser.CurrentGun.SetShoot(true);

            _playerRotater.RotateToPoint(currentEnemy.position, Vector3.up, WorldAxis.Y);

            yield return _waitForShootingUpdate;
        }
    }

    private Transform GetNearestEnemy()
    {
        if (_enemyes == null || _enemyes.Length == 0)
        {
            return null;
        }

        int nearestEnemyIndex = 0;
        float distanceToNearest;

        if (_enemyes[0] == null)
        {
            distanceToNearest = float.MaxValue;
        }
        else
        {
            distanceToNearest = Vector3.Distance(_cachedTransform.position, _enemyes[0].position);
        }

        for (int i = 0; i < _enemyes.Length; i++)
        {
            if (_enemyes[i] != null)
            {
                float currentDistance = Vector3.Distance(_cachedTransform.position, _enemyes[i].position);

                if (currentDistance < distanceToNearest)
                {
                    distanceToNearest = currentDistance;
                    nearestEnemyIndex = i;
                }
            }
        }

        return _enemyes[nearestEnemyIndex];
    }
}