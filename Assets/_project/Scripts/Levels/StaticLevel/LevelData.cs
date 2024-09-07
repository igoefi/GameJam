using Ink.Parsed;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField] EnemyStats[] _enemies;
    [SerializeField] Transform[] _enemiesSpawn;
    [SerializeField] float _chanceSpawnEnemies;
    [SerializeField] float _spawnCD;
    [SerializeField] SceneAsset[] _nextScenes;
    [SerializeField] WawesType _wawesType;

    [Min(0)]
    [SerializeField] int _maxEnemies;
    [Min(0)]
    [Header("If Wawes type is wawes, it`s count wawes. Else - it`s enemies count")]
    [SerializeField] int _maxWawes;
    private int _wawesCount;

    private int _enemiesCount;

    private void Awake()
    {
        SpawnWaweEnemies();
    }

    private void EnemyIsDead()
    {
        switch (_wawesType) 
        {
            case WawesType.WawesComing:
                _enemiesCount--;
                if (_enemiesCount <= 0)
                    SpawnWaweEnemies();
                break;
            case WawesType.AlwaysComing:
                if (_enemiesCount >= _maxWawes) return;
                SpawnEnemy();
                break;
        }

    }
    
    private void SpawnEnemy()
    {
        var rand = new System.Random();
        var point = _enemiesSpawn[rand.Next(_enemiesSpawn.Length)];
        var enemy = _enemies[rand.Next(_enemies.Length)];


        Instantiate(enemy,
            point.position, point.rotation).DeadEvent.AddListener(EnemyIsDead);
        _enemiesCount++;
    }

    private void SpawnWaweEnemies()
    {
        if (_wawesCount >= _maxWawes) return;

        _wawesCount++;
        _enemiesCount = 0;

        var spawns = _enemiesSpawn.ToArray();

        for (int i = 0; i < spawns.Length; i++)
        {
            var tr = spawns[i];
            var index = new System.Random().Next(spawns.Length);
            spawns[i] = spawns[index];
            spawns[index] = tr;
        }

        foreach (var point in spawns)
        {
            if (_enemiesCount >= _maxEnemies) break;

            var rand = new System.Random();
            Instantiate(_enemies[rand.Next(_enemies.Length)],
                point.position, point.rotation).DeadEvent.AddListener(EnemyIsDead);
            _enemiesCount++;
        }
    }
}
