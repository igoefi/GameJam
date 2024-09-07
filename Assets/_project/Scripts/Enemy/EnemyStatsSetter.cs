using UnityEngine;
using UnityEngine.AI;

public class EnemyStatsSetter : MonoBehaviour
{
    private EnemyStats _stats;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _stats = GetComponent<EnemyStats>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _agent.speed = _stats.Speed;
        _agent.stoppingDistance = _stats.StopDistance;
    }
}
