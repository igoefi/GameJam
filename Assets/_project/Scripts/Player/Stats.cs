using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public UnityEvent DeadEvent { get; private set; } = new();

    [Header("Base")]
    [SerializeField] protected float _maxHP;
    public float MaxHP { get => _maxHP; }

    protected float _HP;
    public float HP { get => _HP; }

    [SerializeField] protected float _speed;
    public float Speed { get => _speed; }

    [Header("Attack")]

    [SerializeField] protected float _meleeAttackDamage;
    public float MeleeDamage { get => _meleeAttackDamage; }

    [SerializeField] protected float _meleeAttackCD;
    public float MeleeCD { get => _meleeAttackCD; }

    [SerializeField] protected float _meleeAttackRadius;
    public float MeleeRadius { get => _meleeAttackRadius; }

    [SerializeField] protected float _damageMultiply = 1;
    public float DamageMult { get => _damageMultiply; }

    [SerializeField] protected LayerMask _enemyLayer;
    public LayerMask EnemyLayer { get => _enemyLayer; }


    [Header("Weapon")]

    [SerializeField] int _maxWeapons;
    public int MaxWeapons { get => _maxWeapons; }

    [Header("Components")]

    [SerializeField] WeaponSystem _weaponSystem;
    public WeaponSystem WeaponSystem { get => _weaponSystem; }

    public bool CanFire { get; set; } = true;
    public bool CanMove { get; set; } = true;
    public bool CanRotate { get; set; } = true;
    public bool IsDead { get; set; } = false;

    public virtual void Hit(float damage, Stats attacker, Transform attackPoint)
    {
        if (damage <= 0) return;

        _HP = _HP - damage <= 0 ? 0 : _HP - damage;
        if (_HP == 0)
        {
            Dead();
        }
    }

    public virtual void Heal(float heal)
    {
        if (heal <= 0) return;

        _HP = _HP + heal >= _maxHP ? _maxHP : _HP + heal;
    }

    protected virtual void Dead()
    {
        if (IsDead) return;

        IsDead = true;
        DeadEvent.Invoke();
    }
}
