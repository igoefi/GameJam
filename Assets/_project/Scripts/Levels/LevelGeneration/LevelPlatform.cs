using Unity.AI.Navigation;
using UnityEngine;

public class LevelPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] _enemiesPoints;
    [SerializeField] private Transform[] _nextPlatformPositions;

    [SerializeField] private float _chanceSpawn;

    public Transform[] NextPlatformPositions { get => _nextPlatformPositions; }

    public void SetupLevel()
    {
        SpawnAllEnemies();
    }

    public void SetupLevel(float spawnChance)
    {
        SpawnAllEnemies();
    }

    private void SpawnAllEnemies()
    {
        foreach (var point in _enemiesPoints)
            if (Random.value <= _chanceSpawn)
                Instantiate(LevelPlatformData.Instance.GetRandomEnemy(), point.position, point.rotation);
    }
    private void SpawnAllEnemies(float spawnChance)
    {
        foreach (var point in _enemiesPoints)
            if (Random.value <= spawnChance)
                Instantiate(LevelPlatformData.Instance.GetRandomEnemy(), point.position, point.rotation);
    }
}
