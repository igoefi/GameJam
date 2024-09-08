using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingWeapon : Weapon
{
    [SerializeField] protected float _realoadKD;
    [SerializeField] protected float _bulletSpeed;
    [SerializeField] protected float _bulletLifeTime;

    [SerializeField] protected Bullet _bulletPrefab;
    [SerializeField] protected Transform _bulletPosition;

    [SerializeField] protected int _maxAmmo;
    protected int _currentAmmo;

    [SerializeField] protected int _maxFullAmmo;
    protected int _currentFullAmmo;

    [SerializeField] protected float _maxSpread;

    [SerializeField] protected float _addCoefSpreadByShot;
    [SerializeField] protected float _removeCoefSpreadByKD;
    [SerializeField] protected float _kdSpread;
    protected float _currentSpreadCoef;
    public bool IsReloading { get; protected set; }

    private Coroutine _spreadCorutine;

    public bool IsEmpty() =>
    _currentAmmo <= 0;

    public (int, int) GetAmmoInfo() => (_currentAmmo, _currentFullAmmo);

    public override string GetInfo() =>
        $"{_name}\n{_currentAmmo} / {_maxAmmo}\n{_currentFullAmmo} / {_maxFullAmmo}";

    public override void Preparation()
    {
        _currentAmmo = _maxAmmo;
        _currentFullAmmo = _maxFullAmmo;
    }
    public void Preparation((int, int) ammoData)
    {
        _currentAmmo = ammoData.Item1;
        _currentFullAmmo = ammoData.Item2;
    }

    public override bool Attack(bool isPressed, float damageMult, LayerMask mask, Stats attacker)
    {
        if (!_isReady) return false;
        if (_currentAmmo <= 0) return false;
        if (!isPressed) return false;

        Shoot(damageMult, mask, attacker);

        StartCoroutine(CheckCD(_CD));
        return true;
    }

    public virtual void Reload()
    {
        if (!_isReady) return;
        if (_currentAmmo >= _maxAmmo + 1) return;
        if (_currentFullAmmo <= 0) return;

        StartCoroutine(ReloadCorutine());
    }

    protected virtual void Shoot(float damageMult, LayerMask mask, Stats attacker)
    {
        if (_currentAmmo <= 0) return;
        _currentAmmo--;

        SetSpreadCoef();

        var spread = Random.value * _maxSpread * _currentSpreadCoef;
        spread *= Random.value > 0.5 ? 1 : -1;

        var rotation = _bulletPosition.rotation * Quaternion.Euler(0, spread, 0);
        Instantiate(_bulletPrefab, _bulletPosition.position, rotation)
        .Fire(_damage * damageMult, mask, _bulletSpeed, _bulletLifeTime, attacker);
    }

    protected void SetSpreadCoef()
    {
        _currentSpreadCoef += _addCoefSpreadByShot;
        if (_currentSpreadCoef > 1)
            _currentSpreadCoef = 1;

        if (_spreadCorutine != null)
            StopCoroutine(_spreadCorutine);

        _spreadCorutine = StartCoroutine(RemoveSpread());
    }

    protected virtual IEnumerator RemoveSpread()
    {
        yield return new WaitForSeconds(_kdSpread);

        _currentSpreadCoef -= _removeCoefSpreadByKD;
        if(_currentSpreadCoef < 0) 
            _currentSpreadCoef = 0;


        if (_currentSpreadCoef > 0)
            _spreadCorutine = StartCoroutine(RemoveSpread());
        else
            _spreadCorutine = null;
    }

    protected virtual IEnumerator ReloadCorutine()
    {
        _isReady = false;
        IsReloading = true;

        yield return new WaitForSeconds(_realoadKD);
        _currentFullAmmo += _currentAmmo;
        var needAmmo = _currentAmmo > 0 ? _maxAmmo + 1 : _maxAmmo;

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
