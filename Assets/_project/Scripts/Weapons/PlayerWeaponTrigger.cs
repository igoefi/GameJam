using UnityEngine;

public class PlayerWeaponTrigger : MonoBehaviour
{
    [SerializeField] WeaponSystem _system;
    void Start()
    {
        Inputs.Instance.Fire1.AddListener(_system.Attack);
        Inputs.Instance.Number.AddListener(_system.ChangeWeapon);
        Inputs.Instance.Reload.AddListener(_system.Reload);
        Inputs.Instance.MouseWheelUp.AddListener(_system.ChangeWeapon);
    }
}
