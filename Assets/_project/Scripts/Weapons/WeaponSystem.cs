using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private List<WeaponData> _weapons;
    private int _selectedIndex;
    private Weapon _selectedWeapon;

    [SerializeField] Stats _stats;

    [Header("WeaponPositions")]
    [SerializeField] Transform _pistolPosition;
    [SerializeField] Transform _machineGunPosition;
    [SerializeField] Transform _shotgunPosition;
    [SerializeField] Transform _meleePosition;

    public UnityEvent TryShoot { get; private set; } = new();
    public UnityEvent Shoot { get; private set; } = new();

    public float MoveCoef =>
        _selectedWeapon.MoveCoefficient;
    public Weapon SelectedWeapon =>
        _selectedWeapon;
    public bool IsReady { get => _selectedWeapon.IsReady; }

    public bool IsReloading()
    {
        var weapon = GetShootingWeapon();

        return weapon != null && weapon.IsReloading;
    }
    public bool AmmoIsEmpty()
    {
        var weapon = GetShootingWeapon();

        return weapon != null && weapon.IsEmpty();
    }

    private void Start()
    {
        ChangeWeapon();
    }

    public void Attack(bool isPressed)
    {
        if (!_stats.CanFire) return;
        TryShoot.Invoke();
        if (_selectedWeapon.Attack(isPressed, _stats.DamageMult, _stats.EnemyLayer, _stats))
            Shoot.Invoke();
    }

    public void AddWeapon(Weapon weapon)
    {
        if (_weapons.Count < _stats.MaxWeapons)
        {
            _weapons.Add(new(weapon));
        }
        else
        {
            _weapons[_selectedIndex] = new(weapon);
            ChangeWeapon();
        }
    }

    public void Reload()
    {
        if (_selectedWeapon.IsMeleeWeapon)
            return;

        var weapon = (ShootingWeapon)_selectedWeapon;
        weapon.Reload();
    }
    public void ChangeWeapon(bool isNext)
    {
        if (IsReloading()) return;

        var weapon = GetShootingWeapon();

        _weapons[_selectedIndex].SetData(weapon == null ? (0,0) : weapon.GetAmmoInfo());

        if (isNext)
            _selectedIndex = _selectedIndex + 1 >= _weapons.Count ?
                _selectedIndex = 0 : _selectedIndex + 1;
        else 
            _selectedIndex = _selectedIndex - 1 < 0 ?
                _selectedIndex = _weapons.Count - 1 : _selectedIndex - 1;

        ChangeWeapon();
    }
    public void ChangeWeapon(int num)
    {
        if (IsReloading()) return;


        var weapon = GetShootingWeapon();

        _weapons[_selectedIndex].SetData(weapon == null ? (0, 0) : weapon.GetAmmoInfo());

        num--;
        if (num == _selectedIndex) return;

        if (num >= _weapons.Count)
            num = _weapons.Count - 1;
        else if (num < 0)
            num = 0;

        _selectedIndex = num;

        ChangeWeapon();
    }

    private void ChangeWeapon()
    {
        if (_selectedWeapon != null)
            Destroy(_selectedWeapon.gameObject);
        var weaponPosition = GetWeaponPosition(_weapons[_selectedIndex].Weapon);
        _selectedWeapon = Instantiate(_weapons[_selectedIndex].Weapon,
            weaponPosition.position, weaponPosition.rotation,
            weaponPosition);

        var ammoData = _weapons[_selectedIndex].GetData();

        var weapon = GetShootingWeapon();
        if (weapon == null)
            _selectedWeapon.Preparation();
        else
        {
            if (ammoData.Item1 < 0)
                weapon.Preparation();
            else
                weapon.Preparation(ammoData);
        }
    }

    private Transform GetWeaponPosition(Weapon weapon)
    {
        switch (weapon.Type)
        {
            case WeaponType.Pistol:
                return _pistolPosition;
            case WeaponType.MachineGun:
                return _machineGunPosition;
            case WeaponType.Shotgun:
                return _shotgunPosition;
            case WeaponType.Melee:
                return _meleePosition;

            default: return null;
        }
    }

    private ShootingWeapon GetShootingWeapon()
    {
        if (_selectedWeapon.IsMeleeWeapon) return null;

        return (ShootingWeapon) _selectedWeapon;
    }
}
