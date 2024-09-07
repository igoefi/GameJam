using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class FullLevelGeneration : MonoBehaviour
{
    [SerializeField] NavMeshSurface _surface;
    [SerializeField] Transform _firstLevelPosition;
    [Min(0)]
    [SerializeField] int _maxPlatformsCount;
    private int _platformCount;
    private void Start() =>
        GenerateLevel(_firstLevelPosition);

    private void GenerateLevel(Transform point)
    {
        _platformCount++;
        LevelPlatform platform = null;
        if(_platformCount < _maxPlatformsCount/3)
            platform = Instantiate(LevelPlatformData.Instance.GetPlatformWithCountinue(), point.position, point.rotation);
        else
            platform = Instantiate(LevelPlatformData.Instance.GetRandomPlatform(), point.position, point.rotation);


        foreach (var needPoint in platform.NextPlatformPositions)
        {
            if (_platformCount < _maxPlatformsCount)
                GenerateLevel(needPoint);
            else
            {
                GenerateEndPlatform(needPoint);
                Debug.Log("AAAAAAAAAAA");
            }
        }

        _surface.BuildNavMesh();
    }

    private void GenerateEndPlatform(Transform point) =>
        Instantiate(LevelPlatformData.Instance.GetEndPlatform(), 
            point.position, point.rotation);
}
