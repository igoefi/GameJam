using UnityEngine;

public class AttackZone : MonoBehaviour
{
    private float rad = 1;
    public void Attack(float damage, float radius, LayerMask enemyLayer, Stats attacker)
    {
        rad = radius;
        var hits = Physics.SphereCastAll(transform.position, radius, Vector3.one, radius, enemyLayer);

        foreach (var hit in hits)
            if (hit.collider.TryGetComponent(out Stats stats))
                stats.Hit(damage, attacker, transform);
    }
}
