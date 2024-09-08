using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSetter : MonoBehaviour
{
    [SerializeField] DialogueNPC _npc;
    void Start()
    {
        _npc.StartDialogue();
        DialogueTaling.Instance.NextPhrase();
    }
}
