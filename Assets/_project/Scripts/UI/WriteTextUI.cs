using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WriteTextUI : MonoBehaviour
{
    [SerializeField] TMP_Text _text;

    private Coroutine _write;

    public void WriteText(string text, float time)
    {
        if (_write != null)
            StopCoroutine(_write);

        _write = StartCoroutine(Write(text, time));
    }

    private IEnumerator Write(string text, float time)
    {
        _text.text = "";

        foreach(var c in text)
        {
            _text.text += c;
            yield return new WaitForSeconds(time);
        }
    }
}
