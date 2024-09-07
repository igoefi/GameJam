using UnityEngine;
using UnityEngine.Events;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract Instance { get; private set; }

    public static UnityEvent StartInteractEvent { get; private set; } = new();
    public static UnityEvent EndInteractEvent { get; private set; } = new();

    private bool _isInteracting;
    private ObjectInteract _interatableObject;

    private void Awake() =>
        Instance = this;

    private void Start()
    {
        Inputs.Instance.Interact.AddListener(Interact);

        StartInteractEvent.AddListener(() => PlayerStats.Instance.CanMove = false);
        EndInteractEvent.AddListener(() => PlayerStats.Instance.CanMove = true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent<ObjectInteract>(out var obj)) return;

        _interatableObject = obj;
        InteractUI.Instance.SetText(obj.Name, obj.Verb);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_interatableObject == null) return;

        if (other.gameObject == _interatableObject.gameObject)
            _interatableObject = null;

        InteractUI.Instance.Hide();
    }

    private void Interact()
    {
        if (_interatableObject == null) return;
        if (_isInteracting) return;

        _isInteracting = true;

        StartInteractEvent.Invoke();
        _interatableObject.Interact();
        _interatableObject = null;
        InteractUI.Instance.Hide();
    }

    public void EndInteract()
    {
        _isInteracting = false;
        EndInteractEvent.Invoke();
    }
}