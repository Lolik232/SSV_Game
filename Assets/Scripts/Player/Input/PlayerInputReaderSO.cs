using System;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "PlayerInputReader", menuName = "Player/Input/Reader")]
public class PlayerInputReaderSO : ScriptableObject
{
    private PlayerInput _playerInput;

    private Camera _mainCamera;
    private Vector2 _dashInput;

    public UnityAction<Vector2Int> MoveEvent = delegate { };
    public UnityAction GrabEvent = delegate { };
    public UnityAction GrabCanceledEvent = delegate { };
    public UnityAction<Vector2> DashEvent = delegate { };
    public UnityAction DashCanceledEvent = delegate { };
    public UnityAction JumpEvent = delegate { };
    public UnityAction JumpCanceledEvent = delegate { };

    private void OnEnable()
    {
        _mainCamera = Camera.main;
    }

    public void InitializePlayerInput(PlayerInput playerInput)
    {
        _playerInput = playerInput;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        static Vector2Int NormalizeMovementInput(Vector2 movementInput)
        {
            int normInputX = Mathf.Abs(movementInput.x) > 0.5f ? (int)(movementInput * Vector2.right).normalized.x : 0;
            int normInputY = Mathf.Abs(movementInput.y) > 0.5f ? (int)(movementInput * Vector2.up).normalized.y : 0;
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
            DashEvent.Invoke(_dashInput);
        }
        else if (context.canceled)
        {
            DashCanceledEvent.Invoke();
        }
    }

    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        if (_playerInput.currentControlScheme == "Keyboard")
        {
            _dashInput = _mainCamera.ScreenToWorldPoint((Vector3)context.ReadValue<Vector2>());
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumpEvent.Invoke();
        }
        else if (context.canceled)
        {
            JumpCanceledEvent.Invoke();
        }
    }
}
