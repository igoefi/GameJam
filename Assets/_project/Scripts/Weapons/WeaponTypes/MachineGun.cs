using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : ShootingWeapon
{
    public new WeaponType Type = WeaponType.MachineGun;

    protected Coroutine _shootCorutine;

    public override bool Attack(bool isPressed, float damageMult, LayerMask mask, Stats attacker)
    {
        if(isPressed)
            StartShooting(damageMult, mask, attacker);
        else
            StopShooting();

        return true;
    }

    protected virtual void StartShooting(float damageMult, LayerMask mask, Stats attacker)
    {
        if(_shootCorutine == null)
            _shootCorutine = StartCoroutine(ShootCorutine(damageMult, mask, attacker));
    }

    protected virtual void StopShooting()
    {
        if (_shootCorutine == null) return;
        StopCoroutine(_shootCorutine);
        _shootCorutine = null;
    }

    protected virtual IEnumerator ShootCorutine(float damageMult, LayerMask mask, Stats attacker)
    {
        Shoot(damageMult, mask, attacker);
        yield return new WaitForSeconds(_CD);
        if (_currentAmmo > 0)
            _shootCorutine = StartCoroutine(ShootCorutine(damageMult, mask, attacker));
        else
            _shootCorutine = null;
    }
}
