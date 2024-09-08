using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabledUI : MonoBehaviour
{
    [SerializeField] GameObject[] _UI;
    private bool _isFirst = true;
    private void Awake()
    {
        foreach (var ui in _UI)
            ui.SetActive(true);
    }
    private void Update()
    {
        if (!_isFirst) return;


        foreach (var ui in _UI)
            ui.SetActive(false);
        _isFirst = false;
    }
}
