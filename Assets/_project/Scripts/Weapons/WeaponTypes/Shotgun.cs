using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : ShootingWeapon
{
    public new WeaponType Type = WeaponType.Shotgun;

    [SerializeField] int _pelletCount;

    protected override void Shoot(float damageMult, LayerMask mask, Stats attacker)
    {
        if (_currentAmmo <= 0) return;
        _currentAmmo--;
        for (int i = 0; i < _pelletCount; i++)
        {
            SetSpreadCoef();

            var spread = Random.value * _maxSpread * _currentSpreadCoef;
            spread *= Random.value > 0.5 ? 1 : -1;

            var rotation = _bulletPosition.rotation * Quaternion.Euler(0, spread, 0);
            Instantiate(_bulletPrefab, _bulletPosition.position, rotation)
            .Fire(_damage * damageMult, mask, _bulletSpeed, _bulletLifeTime, attacker);

        }
    }
}
