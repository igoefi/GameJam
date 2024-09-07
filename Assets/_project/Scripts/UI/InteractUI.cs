using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    public static InteractUI Instance { get; private set; }

    [SerializeField] TMP_Text _name;
    [SerializeField] TMP_Text _verb;

    private void Awake() =>
        Instance = this;

    private void Start() =>
        Hide();

    public void SetText(string name, string verb)
    {
        gameObject.SetActive(true);
        _name.text = name;
        _verb.text = verb;
    }

    public void Hide() =>
        gameObject.SetActive(false);

}
