using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTaling : MonoBehaviour
{
    public static DialogueTaling Instance { get; private set; }

    private Story _story;
    private DialogueNPC _NPC;

    private void Awake() =>
        Instance = this;

    private void Start()
    {
        Inputs.Instance.Interact.AddListener(NextPhrase);
        Inputs.Instance.Number.AddListener(SetChoise);
    }
    public void StartDialogue(Story dialogue, DialogueNPC NPC)
    {
        _story = dialogue;
        _NPC = NPC;
    }

    public void SetChoise(int num)
    {
        if (_story == null) return;
        if (_story.currentChoices.Count == 0) return;

        var index = num - 1;
        if (index < 0)
            index = 0;

        _story.ChooseChoiceIndex(index);
        NextPhrase();
    }

    private void NextPhrase()
    {
        if (_story == null) return;

        var text = LoadPhrase();
        if (text == null) return;

        var name = GetVariable(DialogueVariables.SpeakerVar);
        var speed = float.Parse(GetVariable(DialogueVariables.SpeedVar));
        var sprite = _NPC.GetSprite(GetVariable(DialogueVariables.SpriteVar));
        var audio = _NPC.GetAudio(GetVariable(DialogueVariables.AudioVar));

        DialogueUI.Instance.SetPhrase(name, text, speed, sprite, audio);
    }

    private string LoadPhrase()
    {
        if (_story.currentChoices.Count > 0)
        {
            DialogueUI.Instance.SetChoices(_story.currentChoices);
            return null;
        }
        else if (_story.canContinue)
            return _story.Continue();
        else
            EndDialogue();
        return null;
    }

    private void EndDialogue()
    {
        _story = null;
        DialogueUI.Instance.Hide();

        PlayerInteract.Instance.EndInteract();
    }

    private string GetVariable(string name)
    {
        if (_story == null) return null;

        return _story.variablesState.GetVariableWithName(name).ToString();
    }
}
