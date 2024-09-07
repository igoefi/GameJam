using System.Collections.Generic;
using UnityEngine;

public class LevelPlatformData : MonoBehaviour
{
    public static LevelPlatformData Instance { get; private set; }

    [SerializeField] EnemyStats[] _enemies;
    [SerializeField] LevelPlatform[] _platforms;

    private List<LevelPlatform> _endPlatforms = new();
    private List<LevelPlatform> _countinuePlatforms = new();

    private void Awake()
    {
        Instance = this;
        SetSpecialArrays();
    }
    private void SetSpecialArrays()
    {
        foreach(var platform in _platforms)
            if(platform.NextPlatformPositions.Length == 0)
                _endPlatforms.Add(platform);
            else
                _countinuePlatforms.Add(platform);

    }

    public EnemyStats GetRandomEnemy() =>
        _enemies[new System.Random().Next(_enemies.Length)];
    public LevelPlatform GetRandomPlatform() =>
        _platforms[new System.Random().Next(_platforms.Length)];
    public LevelPlatform GetPlatformWithCountinue() =>
        _countinuePlatforms[new System.Random().Next(_countinuePlatforms.Count)];
    public LevelPlatform GetEndPlatform() =>
        _endPlatforms[new System.Random().Next(_endPlatforms.Count)];
}
