using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    public UnityEvent FrontOfPlayer{ get; private set; } = new();
    public UnityEvent MeleeOfEnemy{ get; private set; } = new();

    private NavMeshAgent _navMeshAgent;
    private EnemyStats _stats;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _stats = GetComponent<EnemyStats>();
    }

    private void Update()
    {
        if (_stats.Target == null) return;
        if (_navMeshAgent.isStopped) return;

        _navMeshAgent.SetDestination(_stats.Target.position);

        var distance = Vector3.Distance(transform.position, _stats.Target.position);

        if(distance <= _stats.MeleeDistance)
            MeleeOfEnemy.Invoke(); 
        else if (distance <= _navMeshAgent.stoppingDistance)
            FrontOfPlayer.Invoke();
    }

    public void StartMoving() =>
        _navMeshAgent.isStopped = false;

    public void StopMoving() =>
        _navMeshAgent.isStopped = true;
}
