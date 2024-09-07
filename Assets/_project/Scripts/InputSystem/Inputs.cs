using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    public static Inputs Instance { get; private set; }

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public UnityEvent<bool> Dash { get; set; } = new();
    public UnityEvent Interact { get; private set; } = new();
    public UnityEvent MeleeAttack { get; private set; } = new();
    public UnityEvent<bool> Fire1 { get; private set; } = new();
    public UnityEvent<bool> Fire2 { get; private set; } = new();
    public UnityEvent<bool> MouseWheelUp { get; private set; } = new();
    public UnityEvent<int> Number { get; private set; } = new();
    public UnityEvent Reload { get; private set; } = new();

    public bool LeftChoiceButton { get; private set; }
    public bool RightChoiceButton { get; private set; }
    public bool TopChoiceButton { get; private set; }

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    private void Awake() =>
        Instance = this;

    private void Update() =>
        SetCursorState();

    #region InputMethods

    public void OnMove(InputValue value) =>
        MoveInput(value.Get<Vector2>());

    public void OnLook(InputValue value) =>
        LookInput(value.Get<Vector2>());

    public void OnDash(InputValue value) =>
        SetButton(ButtonType.Dash, value.isPressed);
    public void OnInteract(InputValue value) =>
        SetButton(ButtonType.Interact, value.isPressed);
    public void OnMeleeAttack(InputValue value) =>
        SetButton(ButtonType.Melee, value.isPressed);
    public void OnFire1(InputValue value) =>
        SetButton(ButtonType.Fire1, value.isPressed);
    public void OnFire2(InputValue value) =>
        SetButton(ButtonType.Fire2, value.isPressed);
    public void On_1(InputValue value) =>
        SetButton(ButtonType.Num_1, value.isPressed);
    public void On_2(InputValue value) =>
        SetButton(ButtonType.Num_2, value.isPressed);
    public void On_3(InputValue value) =>
        SetButton(ButtonType.Num_3, value.isPressed);
    public void On_4(InputValue value) =>
        SetButton(ButtonType.Num_4, value.isPressed);
    public void On_5(InputValue value) =>
        SetButton(ButtonType.Num_5, value.isPressed);
    public void On_6(InputValue value) =>
        SetButton(ButtonType.Num_6, value.isPressed);
    public void On_7(InputValue value) =>
        SetButton(ButtonType.Num_7, value.isPressed);
    public void On_8(InputValue value) =>
        SetButton(ButtonType.Num_8, value.isPressed);
    public void On_9(InputValue value) =>
        SetButton(ButtonType.Num_9, value.isPressed);
    public void On_0(InputValue value) =>
        SetButton(ButtonType.Num_0, value.isPressed);
    public void OnReload(InputValue value) =>
        SetButton(ButtonType.Reload, value.isPressed);
    public void OnMouseWheelUp(InputValue value) =>
        SetButton(ButtonType.MouseWheelUp, value.isPressed);
    public void OnMouseWheelDown(InputValue value) =>
        SetButton(ButtonType.MouseWheelDown, value.isPressed);
    #endregion

    public void MoveInput(Vector2 newMoveDirection) =>
        Move = newMoveDirection;

    public void LookInput(Vector2 newLookDirection) =>
        Look = newLookDirection;

    private void SetButton(ButtonType type, bool state)
    {
        switch (type)
        {
            case ButtonType.Dash:
                Dash.Invoke(state);
                break;
            case ButtonType.Interact:
                if (state == true)
                    Interact.Invoke();
                break;
            case ButtonType.Melee:
                if (state == true)
                    MeleeAttack.Invoke();
                break;
            case ButtonType.Fire1:
                    Fire1.Invoke(state);
                break;
            case ButtonType.Fire2:
                    Fire2.Invoke(state);
                break;
            case ButtonType.Reload:
                if (state == true)
                    Reload.Invoke();
                break;
            case ButtonType.Num_0:
                if (state == true)
                    Number.Invoke(0);
                break;
            case ButtonType.Num_1:
                if (state == true)
                    Number.Invoke(1);
                break;
            case ButtonType.Num_2:
                if (state == true)
                    Number.Invoke(2);
                break;
            case ButtonType.Num_3:
                if (state == true)
                    Number.Invoke(3);
                break;
            case ButtonType.Num_4:
                if (state == true)
                    Number.Invoke(4);
                break;
            case ButtonType.Num_5:
                if (state == true)
                    Number.Invoke(5);
                break;
            case ButtonType.Num_6:
                if (state == true)
                    Number.Invoke(6);
                break;
            case ButtonType.Num_7:
                if (state == true)
                    Number.Invoke(7);
                break;
            case ButtonType.Num_8:
                if (state == true)
                    Number.Invoke(8);
                break;
            case ButtonType.Num_9:
                if (state == true)
                    Number.Invoke(9);
                break;
            case ButtonType.MouseWheelUp:
                if (state == true)
                    MouseWheelUp.Invoke(true);
                break;
            case ButtonType.MouseWheelDown:
                if (state == true)
                    MouseWheelUp.Invoke(false);
                break;
        }
    }

    private void SetCursorState()
    {
        Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !cursorLocked;
    }

    public void LockCursor(bool isLock)
    {
        cursorLocked = isLock;
        cursorInputForLook = isLock;
    }
}

internal enum ButtonType
{
    Dash,
    Sprint,
    Sit,
    Interact,
    Melee,
    E,
    Q,
    Fire1,
    Fire2,
    Reload,
    UIClose,
    UIBack,
    Num_1,
    Num_2,
    Num_3,
    Num_4,
    Num_5,
    Num_6,
    Num_7,
    Num_8,
    Num_9,
    Num_0,
    MouseWheelUp,
    MouseWheelDown,
}