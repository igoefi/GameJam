using Ink.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance { get; private set; }

    [SerializeField] private TMP_Text _name;
    [SerializeField] private WriteTextUI _text;

    [SerializeField] private TMP_Text _choises;

    [SerializeField] AudioSource _audio;
    [SerializeField] Image _image;

    private void Awake() =>
        Instance = this;

    private void Start() =>
        gameObject.SetActive(false);

    public void SetPhrase(string name, string text, float time, Sprite sprite, AudioClip audio)
    {
        gameObject.SetActive(true);
        SetUI(name, text, time, sprite);
        SetAudio(audio);
    }

    public void SetChoices(List<Choice> choices)
    {
        _choises.text = "";
        for (int i = 1; i < choices.Count + 1; i++)
            _choises.text += $"{i}: {choices[i-1].text}\n";
    }

    private void SetUI(string name, string text, float time, Sprite sprite)
    {
        _name.text = name;
        _text.WriteText(text, time);
        _image.sprite = sprite;
        _choises.text = "";
    }

    private void SetAudio(AudioClip audio)
    {
        _audio.clip = audio;
        _audio.Play();
    }

    public void Hide() =>
        gameObject.SetActive(false);

}
