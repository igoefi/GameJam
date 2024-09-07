using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.UI;

public abstract class Weapon : KDTimer
{
    [SerializeField] protected string _name;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _CD;

    [SerializeField] protected float _moveCoefficient = 1;

    public WeaponType Type { get; protected set; }

    public float MoveCoefficient { get => _moveCoefficient; }

    public bool IsMeleeWeapon { get; protected set; }
    public bool IsReady { get => _isReady; }

    public virtual string GetInfo() => _name;

    public abstract bool Attack(bool isPressed, float damageMult, LayerMask mask, Stats attacker);

    public abstract void Preparation();
}

public enum WeaponType
{
    Pistol,
    MachineGun,
    Shotgun,
    Melee
}