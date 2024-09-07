using System.Collections;
using UnityEngine;

public class EnemyWithWeapon : KDTimer
{
    private EnemyAI _AI;
    private EnemyStats _stats;

    [SerializeField] private WeaponSystem _system;

    private void Start()
    {
        _AI = GetComponent<EnemyAI>();
        _stats = GetComponent<EnemyStats>();

        _AI.FrontOfPlayer.AddListener(Attack);
        _AI.MeleeOfEnemy.AddListener(MeleeAttack);
    }

    private void Update()
    {
        if (_system.AmmoIsEmpty())
            _system.Reload();
    }

    private void Attack()
    {
        if (!_system.IsReady) return;

        transform.LookAt(_stats.Target);
        _system.Attack(true);
    }

    private void MeleeAttack()
    {
        Debug.Log("Melee");
    }

    protected override IEnumerator CheckCD(float timeKD)
    {
        _AI.StopMoving();
        _isReady = false;

        yield return new WaitForSeconds(timeKD);

        _AI.StartMoving();
        _isReady = true;
    }
}
