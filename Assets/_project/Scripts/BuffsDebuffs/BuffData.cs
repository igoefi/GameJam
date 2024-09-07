using System;

[Serializable]
public class BuffData
{
    public VarBuffEnum VarEnum;
    public TimeBuffEnum TimeEnum;
    public TypeBuffEnum TypeEnum;

    public float Value;
    public float Time;
    public bool IsDestroy = true;
}
