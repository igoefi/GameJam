using Unity.Mathematics;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimationsController : MonoBehaviour
{
    static string _speedName = "Speed";
    static string _shootName = "Shoot";

    [SerializeField] Animator _anim;
    [SerializeField] Rigidbody _body;

    private void Start()
    {
        PlayerStats.Instance.WeaponSystem.Shoot.AddListener(Shoot);
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
    private void Shoot() =>
        _anim.SetTrigger(_shootName);
}
