using UnityEngine;
using System;
using Zenject;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawnChanceObject[] _spawnChanceObject;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField][Min(1)] private int _targetEnemyesCount;

    private SignatureMarcup_Enemy[] _spawnPrefabsArray;
    private MonoFactory<SignatureMarcup_Enemy> _enemyFactory;

    private readonly Vector3 SPAWN_SPRED = new Vector3(2, 2, 2);

    [Inject] private DiContainer _container;

    private void Start()
    {
        _enemyFactory = new MonoFactory<SignatureMarcup_Enemy>(_container);
        _spawnPrefabsArray = GenerateSpawnArray();

        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < _targetEnemyesCount; i++)
        {
            _enemyFactory.CreateWithoutParent(
                    _spawnPrefabsArray[Random.Range(0, _spawnPrefabsArray.Length)],
                    _spawnPoints[Random.Range(0, _spawnPoints.Length)].position + new Vector3(Random.Range(-SPAWN_SPRED.x, SPAWN_SPRED.x), Random.Range(-SPAWN_SPRED.y, SPAWN_SPRED.y), Random.Range(-SPAWN_SPRED.z, SPAWN_SPRED.z)),
                    Quaternion.identity
                );
        }
    }

    private SignatureMarcup_Enemy[] GenerateSpawnArray()
    {
        List<SignatureMarcup_Enemy> spawnList = new List<SignatureMarcup_Enemy>();

        for (int i = 0; i < _spawnChanceObject.Length; i++)
        {
            for (int j = 0; j < _spawnChanceObject[i].SpawnChance; j++)
            {
                spawnList.Add(_spawnChanceObject[i].CurrentEnemyPrefab);
            }
        }

        return spawnList.ToArray();
    }
}

[Serializable]
public class EnemySpawnChanceObject
{
    [SerializeField] private SignatureMarcup_Enemy _currentEnemyPrefab;
    [SerializeField][Min(1)] private int _spawnChance = 1;

    public SignatureMarcup_Enemy CurrentEnemyPrefab => _currentEnemyPrefab;
    public int SpawnChance => _spawnChance;
}