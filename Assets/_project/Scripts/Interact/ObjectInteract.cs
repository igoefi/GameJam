using UnityEngine;
using UnityEngine.Events;

public class ObjectInteract : MonoBehaviour
{
    [SerializeField] UnityEvent _interactEvent = new();

    [SerializeField] string _name;
    public string Name { get => _name; }

    [SerializeField] string _verb;
    public string Verb { get => _verb; }

    public void Interact() =>
        _interactEvent.Invoke();


}
