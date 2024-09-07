using System.Collections;
using UnityEngine;

public class EnemyMeleeAttack : KDTimer
{
    private EnemyAI _AI;
    private EnemyStats _stats;

    [SerializeField] AttackZone _attack;

    private void Start()
    {
        _AI = GetComponent<EnemyAI>();
        _stats = GetComponent<EnemyStats>();

        _AI.FrontOfPlayer.AddListener(Attack);
    }

    private void Attack()
    {
        if (!_isReady) return;
        var damage = _stats.MeleeDamage * _stats.DamageMult;
        transform.LookAt(_stats.Target);
        _attack.Attack(damage, _stats.MeleeRadius, _stats.EnemyLayer, _stats);

        StartCoroutine(CheckCD(_stats.MeleeCD));
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
