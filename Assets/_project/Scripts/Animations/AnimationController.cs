using Unity.Mathematics;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    static string _speedName = "Speed";
    static string _shootName = "Shoot";

    [SerializeField] Animator _anim;
    [SerializeField] Rigidbody _body;

    private Stats _stats;

    private void Start()
    {
        _stats = PlayerStats.Instance;
        _stats.WeaponSystem.Shoot.AddListener(ShootAnimation);
    }

    private void Update()
    {
        SetRun();
    }

    private void SetRun()
    {
        if (math.abs(_body.velocity.x) > 0 || math.abs(_body.velocity.z) > 0)
            _anim.SetFloat(_speedName, 1);
        else
            _anim.SetFloat(_speedName, 0);
    }
    private void ShootAnimation() =>
        _anim.SetTrigger(_shootName);


#region From Animation
    public void FASetMove(bool isTrue) =>
        _stats.CanMove = isTrue;
    public void FAShoot() =>
        _stats.WeaponSystem.Attack(true);

#endregion
}
