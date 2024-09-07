using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public static PlayerRotation Instance { get; private set; }

    [SerializeField] private Transform _camera;
    [SerializeField] LayerMask _levelMask;
    private Rigidbody _body;

    private bool _isCoursorRotation; 

    private void Start()
    {
        _body = PlayerStats.Instance.Rigidbody;    
        Inputs.Instance.Fire1.AddListener(SetRotation);
        PlayerStats.Instance.WeaponSystem.TryShoot.AddListener(RotateOnCursor);
    }

    private void RotateOnCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Inputs.Instance.Look);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, _levelMask))
        {
            Vector3 forward = transform.position - hit.point;
            forward.y = 0;
            transform.rotation = Quaternion.LookRotation(-forward.normalized, Vector3.up);
            Debug.DrawLine(_camera.position, hit.point);
        }
    }

    private void RotateOnVelocity()
    {
        var direction = _body.velocity.normalized;

        if (direction == Vector3.zero) return;
        var rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.2f);
        transform.rotation = rot;
    }

    private void SetRotation(bool IsOnCursor) =>
        _isCoursorRotation = IsOnCursor;

    private void FixedUpdate()
    {
        if (!PlayerStats.Instance.CanMove) return;

        if (_isCoursorRotation)
            RotateOnCursor();
        else
            RotateOnVelocity();
    }
}