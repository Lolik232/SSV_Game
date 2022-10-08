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
    private Vector2 _inputDirection;

    public UnityAction<Vector2Int> MoveEvent = delegate { };
    public UnityAction GrabEvent = delegate { };
    public UnityAction GrabCanceledEvent = delegate { };
    public UnityAction<Vector2> DashEvent = delegate { };
    public UnityAction DashCanceledEvent = delegate { };
    public UnityAction<Vector2> AbilityEvent = delegate { };
    public UnityAction AbilityCanceledEvent = delegate { };
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
        MoveEvent.Invoke(Vector2Int.RoundToInt(context.ReadValue<Vector2>()));
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.performed)
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
        if (context.performed)
        {
            DashEvent.Invoke(_inputDirection);
        }
        else if (context.canceled)
        {
            DashCanceledEvent.Invoke();
        }
    }

    public void OnAbilityInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            AbilityEvent.Invoke(_inputDirection);
        }
        else if (context.canceled)
        {
            AbilityCanceledEvent.Invoke();
        }
    }

    public void OnDirectionInput(InputAction.CallbackContext context)
    {
        if (_playerInput.currentControlScheme == "Keyboard")
        {
            _inputDirection = _mainCamera.ScreenToWorldPoint((Vector3)context.ReadValue<Vector2>());
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
