using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    private EnemyStats _stats;

    private void Awake() =>
        _stats = GetComponent<EnemyStats>();

    private void Update()
    {
        if (_stats.Target != null)
        {
            enabled = false;
            return;
        }

        var player = RayToScan();
        if (player != null)
        {
            _stats.SetTarget(player);
            enabled = false;
        }
    }

    private Transform RayToScan()
    {
        float j = 0;
        for (int rayNum = 0; rayNum < _stats.Rays; rayNum++)
        {
            var sin = Mathf.Sin(j);
            var cos = Mathf.Cos(j);

            j += _stats.Angle * Mathf.Deg2Rad / _stats.Rays;

            Vector3 direction = transform.TransformDirection(new Vector3(sin, 0, cos));

            var player = GetRaycast(direction);
            if (player != null) return player;

            if (sin != 0)
            {
                direction = transform.TransformDirection(new Vector3(-sin, 0, cos));
                player = GetRaycast(direction);
                if (player != null) return player;
            }
        }

        return null;
    }

    private Transform GetRaycast(Vector3 dir)
    {
        Vector3 pos = transform.position + _stats.Offset;
        if (Physics.Raycast(pos, dir, out RaycastHit hit, _stats.Distance, _stats.EnemyLayer))
            return hit.transform;
        return null;
    }

    
}