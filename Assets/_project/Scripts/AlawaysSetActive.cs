using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlawaysSetActive : MonoBehaviour
{
    [SerializeField] GameObject[] _objects;
    void Update()
    {
        foreach (var o in _objects)
            o.SetActive(true);
    }
}
