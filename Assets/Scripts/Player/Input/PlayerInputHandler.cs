using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }

    public Int32 NormInutX { get; private set; }

    public Int32 NormInutY { get; private set; }

    public Boolean GrabInput { get; private set; }

    public Boolean JumpInput { get; private set; }
    public Boolean JumpInputStop { get; private set; }

    [SerializeField]
    private Single _inputHoldTime = 0.1f;

    private Single _inputInputStartTime;

    private void Update()
    {
        CheckJumpInputHoldTime();
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

    public void UseJumpInput() => JumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= _inputInputStartTime + _inputHoldTime)
        {
            JumpInput = false;
        }
    }
}
