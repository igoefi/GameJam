using Ink.Runtime;
using System.Linq;
using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    [SerializeField] TextAsset[] _dialogues;
    [SerializeField] Sprite[] _sprites;
    [SerializeField] AudioClip[] _phrases;
    private int _index;

    public void StartDialogue()
    {
        if (_index >= _dialogues.Count())
            _index = _dialogues.Count() - 1;
        DialogueTaling.Instance.StartDialogue(new Story(_dialogues[_index].text), this);
    }

    public Sprite GetSprite(string name)
    {
        foreach (var sprite in _sprites)
        {
            if (sprite.name == name)
                return sprite;
        }
        return null;
    }
    public AudioClip GetAudio(string name)
    {
        foreach (var clip in _phrases)
        {
            if (clip.name == name)
                return clip;
        }
        return null;
    }
}
