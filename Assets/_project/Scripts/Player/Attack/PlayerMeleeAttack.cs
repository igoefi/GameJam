using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : KDTimer
{
    [SerializeField] AttackZone _zone;

    private PlayerStats _stats;

    private void Start()
    {
        Inputs.Instance.MeleeAttack.AddListener(Attack);
        _stats = PlayerStats.Instance;
    }

    private void Attack()
    {
        if (!_isReady) return;
        var damage = _stats.MeleeDamage * _stats.DamageMult;
        _zone.Attack(damage, _stats.MeleeRadius, _stats.EnemyLayer, _stats);

        StartCoroutine(CheckCD(_stats.MeleeCD));
    }
}
