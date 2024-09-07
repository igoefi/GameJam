using Ink.Runtime;
using UnityEngine;

public class TryGetGlobals : MonoBehaviour
{
    [SerializeField] TextAsset _story;

    private void Start()
    {
        var story = new Story(_story.text);
        while (story.canContinue)
        {
            foreach (var name in story.variablesState)
            {
                var value = story.variablesState.GetVariableWithName(name);
                Debug.Log($"{name} : {value}");
            }
            story.Continue();
        }
    }
}
