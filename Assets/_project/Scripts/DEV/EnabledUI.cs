using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabledUI : MonoBehaviour
{
    [SerializeField] GameObject[] _UI;

    private void Awake()
    {
        foreach (var ui in _UI)
            ui.SetActive(true);
    }
    private void Start()
    {
        foreach (var ui in _UI)
            ui.SetActive(false);
    }
}
