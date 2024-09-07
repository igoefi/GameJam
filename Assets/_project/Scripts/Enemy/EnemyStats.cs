using Unity.Mathematics;
using UnityEngine;

public class EnemyStats : Stats
{
    [Header("Vision")]

    [SerializeField] int _rays;
    public int Rays { get => _rays; }

    [SerializeField] float _distance;
    public float Distance { get => _distance; }

    [SerializeField] float _angle;
    public float Angle { get => _angle; }

    [SerializeField] Vector3 _offset;
    public Vector3 Offset { get => _offset; }

    [SerializeField] float _stoppingDistance;
    public float StopDistance { get => _stoppingDistance; }

    [SerializeField] float _meleeDistance;
    public float MeleeDistance { get => _meleeDistance; }

    public Transform Target { get; private set; }


    private void Start() =>
        _HP = _maxHP;

    public void SetTarget(Transform target) =>
        Target = target;

    public override void Hit(float damage, Stats attacker, Transform attackPoint)
    {
        base.Hit(damage, attacker, transform);

        var rotation = transform.rotation.eulerAngles;
        var attackerRotation = attackPoint.rotation.eulerAngles;

        var coefRotation = rotation.y / 360;
        var coefAttackerRotation = attackerRotation.y / 360;

        var dif1 = math.abs(coefRotation - coefAttackerRotation);
        var dif2 = coefRotation < coefAttackerRotation ? 
            coefRotation + 1 - coefAttackerRotation : 
            coefAttackerRotation + 1 - coefRotation;
        
        if(dif1 < 0.2 || dif2 < 0.2) 
            base.Hit(damage, attacker, transform);

        Target = attacker.transform;
    }

    protected override void Dead()
    {
        base.Dead();
        Destroy(gameObject);
    }
}
