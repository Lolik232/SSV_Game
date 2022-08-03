using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }

    public Int32 NormInutX { get; private set; }

    public Int32 NormInutY { get; private set; }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInutX = (Int32)(RawMovementInput * Vector2.right).normalized.x;
        NormInutY = (Int32)(RawMovementInput * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        
    }
}
