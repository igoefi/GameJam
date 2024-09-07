using System.Runtime.CompilerServices;
using UnityEngine;
public class PlayerStats : Stats
{
    public static PlayerStats Instance { get; private set; }

    [Header("Movement")]
    [SerializeField] float _dashForce;
    public float DashForce { get => _dashForce; }

    [SerializeField] float _dashCD;
    public float DashKD { get => _dashCD; }

    [SerializeField] float _dashAddCD;
    public float DashAddKD { get => _dashAddCD; }

    [SerializeField] float _dashCount;
    public float DashCount { get => _dashCount; }
    public float HaveDashCount { get; set; }

    [SerializeField] float _cameraDistance;
    public float CameraDistance { get => _cameraDistance; }

    [Header("Components")]

    [SerializeField] Rigidbody _body;
    public Rigidbody Rigidbody { get => _body; }

    private void Awake() =>
        Instance = this;

    private void Start()
    {
        HaveDashCount = _dashCount;
        _HP = _maxHP;
    }

    public void SetMaximumStats(VarBuffEnum var, TypeBuffEnum type, float value)
    {
        switch (var)
        {
            case VarBuffEnum.HP:
                BuffHP(type, value);
                break;
            case VarBuffEnum.MaxHP:
                SetVariable(ref _maxHP, type, value);
                break;
            case VarBuffEnum.Speed:
                SetVariable(ref _speed, type, value);
                break;
            case VarBuffEnum.DashForce:
                SetVariable(ref _dashForce, type, value);
                break;
            case VarBuffEnum.DashCD:
                SetVariable(ref _dashCD, type, value);
                break;
            case VarBuffEnum.DashAddCD:
                SetVariable(ref _dashAddCD, type, value);
                break;
            case VarBuffEnum.DashCount:
                SetVariable(ref _dashCount, type, value);
                break;
            case VarBuffEnum.CameraDistance:
                SetVariable(ref _cameraDistance, type, value);
                break;
        }
    }
    
    private void BuffHP(TypeBuffEnum type, float value)
    {
        switch (type)
        {
            case TypeBuffEnum.Procent:
                _HP *= value;
                break;
            case TypeBuffEnum.ProcentDiv:
                _HP /= value;
                break;
            case TypeBuffEnum.Add:
                _HP += value;
                break;
            case TypeBuffEnum.Concretly:
                _HP = value;
                break;
        }

        if(_HP <= 0)
        {
            _HP = 0;
            Dead();
        }
        else if(_HP > _maxHP)
        {
            _HP = _maxHP;
        }
    }



    private void SetVariable(ref float var, TypeBuffEnum type, float value)
    {
        switch (type)
        {
            case TypeBuffEnum.Procent:
                var *= value;
                break;
            case TypeBuffEnum.ProcentDiv:
                var /= value;
                break;
            case TypeBuffEnum.Add:
                var += value; 
                break;
            case TypeBuffEnum.Concretly:
                var = value;
                break;
        }
    }
}
