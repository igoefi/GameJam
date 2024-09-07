using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponData
{
    [SerializeField] Weapon _prefab;

    [SerializeField] private int _currentAmmo = -1;
    private int _currentFullAmmo = -1;
    public Weapon Weapon { get => _prefab; }

    public WeaponData(Weapon prefab) =>
        _prefab = prefab;

    public void SetData((int, int) ammoData)
    {
        _currentAmmo = ammoData.Item1;
        _currentFullAmmo = ammoData.Item2;
    }

    public (int, int) GetData() => (_currentAmmo, _currentFullAmmo);
}
