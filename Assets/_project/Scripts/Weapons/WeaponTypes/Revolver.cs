using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : ShootingWeapon
{

    public new WeaponType Type = WeaponType.Pistol;

    protected override IEnumerator ReloadCorutine()
    {
        _isReady = false;
        IsReloading = true;

        yield return new WaitForSeconds(_realoadKD);
        _currentFullAmmo += _currentAmmo;
        var needAmmo = _maxAmmo;

        _currentFullAmmo -= needAmmo;
        if (_currentFullAmmo < 0)
        {
            needAmmo += _currentFullAmmo;
            _currentFullAmmo = 0;
        }

        _currentAmmo = needAmmo;

        IsReloading = false;
        _isReady = true;
    }
}
