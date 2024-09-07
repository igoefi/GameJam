using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffObject : MonoBehaviour
{
    [SerializeField] private BuffData _data;

    public void Activate()
    {
        PlayerBuffs.Instance.ActivateBuff(_data);
        if(_data.IsDestroy)
            Destroy(gameObject);
    }
}
