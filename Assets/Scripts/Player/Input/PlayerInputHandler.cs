using System;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputReader : ScriptableObject
{
    [SerializeField] private Camera _mainCamera;

    public UnityAction<Vector2Int> MoveEvent = delegate { };
    public UnityAction GrabEvent = delegate { };
    public UnityAction GrabCanceledEvent = delegate { };
    public UnityAction DashEvent = delegate { };
    public UnityAction JumpEvent = delegate { };
    public UnityAction JumpCanceledEvent = delegate { };

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2Int NormalizeMovementInput(Vector2 movementInput)
        {
            int normInputX = Mathf.Abs(movementInput.x) > 0.5f ? (int)(movementInput * Vector2.right).normalized.x : 0;
            int normInputY = Mathf.Abs(movementInput.y) > 0.5f ? (int)(movementInput * Vector2.right).normalized.y : 0;
            return new Vector2Int(normInputX, normInputY);
        }

        MoveEvent.Invoke(NormalizeMovementInput(context.ReadValue<Vector2>()));
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabEvent.Invoke();
        }
        else if (context.canceled)
        {
            GrabCanceledEvent.Invoke();
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashEvent.Invoke();
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            JumpEvent.Invoke();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            JumpCanceledEvent.Invoke();
        }
    }
}
