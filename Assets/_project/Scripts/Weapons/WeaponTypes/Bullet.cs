using System.Collections;
using UnityEngine;

public class Bullet : KDTimer
{
    private float _damage;
    private Stats _atacker;
    private bool _isFly;
    private float _speed;
    private float _lifeTime;
    private LayerMask _mask;

    [SerializeField] Rigidbody _body;

    private void FixedUpdate()
    {
        if (!_isFly) return;

        _body.velocity = transform.forward * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == _mask)
        {
            if (other.gameObject.TryGetComponent(out Stats stats))
            {
                if (_damage > 0)
                    stats.Hit(_damage, _atacker, transform);
                else
                    stats.Heal(-_damage);
            }
        }
        Destroy(gameObject);
    }

    public void Fire(float damage, LayerMask enemyMask, float speed, float lifeTime, Stats attacker)
    {
        _damage = damage;
        _atacker = attacker;
        _speed = speed;
        _mask = enemyMask;
        _lifeTime = lifeTime;
        _isFly = true;
        StartCoroutine(DestroyOnEnd());
    }

    private IEnumerator DestroyOnEnd()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
}
