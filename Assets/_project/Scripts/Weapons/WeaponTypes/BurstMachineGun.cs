using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstMachineGun : MachineGun
{
    [SerializeField] int _maxBurstCount;
    [SerializeField] float _burstCD;
    protected int _currentBurstCount;

    protected override void StartShooting(float damageMult, LayerMask mask, Stats attacker)
    {
        base.StartShooting(damageMult, mask, attacker);
    }

    protected override void StopShooting()
    {

    }

    protected override IEnumerator ShootCorutine(float damageMult, LayerMask mask, Stats attacker)
    {
        if (!_isReady) yield break;


        if (_currentBurstCount >= _maxBurstCount)
        {
            StartCoroutine(CheckCD(_burstCD));
            _currentBurstCount = 0;
            _shootCorutine = null;
            yield break;
        }
        _currentBurstCount++;

        yield return base.ShootCorutine(damageMult, mask, attacker);
    }

    //protected override IEnumerator ShootCorutine(float damageMult, LayerMask mask, Stats attacker)
    //{
    //    _currentBurstCount++;

    //    if (_currentBurstCount <= _maxBurstCount)
    //        return base.ShootCorutine(damageMult, mask, attacker);

    //    if (_shootCorutine != null)
    //    {
    //        StopCoroutine(_shootCorutine);
    //        _shootCorutine = null;
    //    }
    //    StartCoroutine(BurstCD(damageMult, mask, attacker));
    //    return null;
    //}

    //protected virtual IEnumerator BurstCD(float damageMult, LayerMask mask, Stats attacker)
    //{
    //    _isReady = false;
    //    yield return new WaitForSeconds(_burstCD);
    //    _currentBurstCount = 0;
    //    if (_isShooting)
    //        _shootCorutine = StartCoroutine(ShootCorutine(damageMult, mask, attacker));
    //    _isReady = true;
    //}
}
