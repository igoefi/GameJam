using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private AttackZone _zone;

    [SerializeField] private float _attackRadius;

    public new WeaponType Type = WeaponType.Melee;

    private void Awake() =>
        IsMeleeWeapon = true;

    public override void Preparation()
    {

    }

    public override bool Attack(bool isPressed, float damageMult, LayerMask mask, Stats attacker)
    {
        if (!isPressed) return false;
        if (!_isReady) return false;

        _zone.Attack(_damage * damageMult, _attackRadius, mask, attacker);
        StartCoroutine(CheckCD(_CD));
        return true;
    }
}
