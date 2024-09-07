using UnityEngine;

public class CameraPoint : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private LayerMask _levelMask;

    private void Update()
    {
        if (!PlayerStats.Instance.CanMove) return;

        var mousePosition = Inputs.Instance.Look;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, _levelMask))
        {
            var dir = hit.point - _player.position;
            var dist = dir.magnitude;
            var normalizedDirection = dir / dist;

            CheckPart(mousePosition, out float x, out float y);
            var distanceX = x * PlayerStats.Instance.CameraDistance;
            var distanceY = y * PlayerStats.Instance.CameraDistance;
            transform.position = _player.position +
                new Vector3(normalizedDirection.x * distanceX, 0,
                normalizedDirection.z * distanceY);
        }
    }

    private void CheckPart(Vector2 position, out float x, out float y)
    {
        var max = new Vector2(Screen.width, Screen.height);

        if (position.x > max.x / 2 && position.y > max.y / 2)
        {
            x = 1 - position.x / (max.x / 2);
            y = 1 - position.y / (max.y / 2);
        }
        else if (position.x < max.x / 2 && position.y > max.y / 2)
        {
            x = position.x / max.x;
            y = 1 - position.y / (max.y / 2);
        }
        else if (position.x < max.x / 2 && position.y < max.y / 2)
        {
            x = position.x / max.x;
            y = position.y / max.y;
        }
        else
        {
            x = 1 - position.x / (max.x / 2);
            y = position.y / max.y;
        }
    }
}
