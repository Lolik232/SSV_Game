using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;

    private Camera _cam;

    public Vector2 RawDashDirectionInput { get; private set; }
    public Vector2Int DashDirectionInput { get; private set; }
    public Vector2 RawMovementInput { get; private set; }

    public Int32 NormInutX { get; private set; }

    public Int32 NormInutY { get; private set; }

    public Boolean GrabInput { get; private set; }
    public Boolean DashInput { get; private set; }
    public Boolean DashInputStop { get; private set; }

    public Boolean JumpInput { get; private set; }
    public Boolean JumpInputStop { get; private set; }

    [SerializeField]
    private Single _inputHoldTime = 0.1f;

    private Single _inputInputStartTime;
    private Single _dashInputStartTime;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _cam = Camera.main;
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        if (Mathf.Abs(RawMovementInput.x) > 0.5f)
        {
            NormInutX = (Int32)(RawMovementInput * Vector2.right).normalized.x;
        }
        else
        {
            NormInutX = 0;
        }

        if (Mathf.Abs(RawMovementInput.y) > 0.5f)
        {
            NormInutY = (Int32)(RawMovementInput * Vector2.up).normalized.y;
        }
        else
        {
            NormInutY = 0;
        }
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabInput = true;
        }
        else if (context.canceled)
        {
            GrabInput = false;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            DashInputStop = false;
            _dashInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            DashInputStop = true;
        }
    }

    public void UseDashInput() => DashInput = false;

    private void CheckDashInputHoldTime()
    {
        if (Time.time >= _dashInputStartTime + _inputHoldTime)
        {
            DashInput = false;
        }
    }

    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        RawDashDirectionInput = context.ReadValue<Vector2>();

        if (_playerInput.currentControlScheme == "Keyboard")
        {
            RawDashDirectionInput = _cam.ScreenToWorldPoint((Vector3)RawDashDirectionInput) - transform.position;
        }

        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            _inputInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void UseJumpInput() => JumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= _inputInputStartTime + _inputHoldTime)
        {
            JumpInput = false;
        }
    }
}
