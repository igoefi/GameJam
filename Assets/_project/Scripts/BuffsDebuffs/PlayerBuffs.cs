using System.Collections;
using UnityEngine;

public class PlayerBuffs : MonoBehaviour
{
    public static PlayerBuffs Instance { get; private set; }

    private PlayerStats _stats;

    private void Awake() =>
        Instance = this;

    private void Start() =>
        _stats = PlayerStats.Instance;


    public void ActivateBuff(BuffData data)
    {
        switch (data.TimeEnum)
        {
            case TimeBuffEnum.Total:
                _stats.SetMaximumStats(data.VarEnum, data.TypeEnum, data.Value);
                break;
            case TimeBuffEnum.Timed:
                _stats.SetMaximumStats(data.VarEnum, data.TypeEnum, data.Value);
                StartCoroutine(TimedBuff(data));
                break;
        }
    }

    private IEnumerator TimedBuff(BuffData data)
    {
        yield return new WaitForSeconds(data.Time);

        switch (data.TypeEnum)
        {
            case TypeBuffEnum.Procent:
                _stats.SetMaximumStats(data.VarEnum, TypeBuffEnum.ProcentDiv, data.Value);
                break;
            case TypeBuffEnum.ProcentDiv:
                _stats.SetMaximumStats(data.VarEnum, TypeBuffEnum.Procent, data.Value);
                break;
            case TypeBuffEnum.Add:
                _stats.SetMaximumStats(data.VarEnum, data.TypeEnum, data.Value * -1);
                break;
        }
    }

}
