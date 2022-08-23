using System;

using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]  private PlayerData m_Data;
    
    public ValueChangingAction<Int32> NormInputX { get; private set; }
    public ValueChangingAction<Int32> NormInputY { get; private set; }

    public TimeDependentAction JumpInput { get; private set; }

    public TriggerAction JumpInputHold { get; private set; }

    public TriggerAction GrabInput { get; private set; }


    private void Awake()
    {
        NormInputX = new ValueChangingAction<Int32>();
        NormInputY = new ValueChangingAction<Int32>();

        JumpInput = new TimeDependentAction(m_Data.jumpInputHoldTime);
        JumpInputHold = new TriggerAction();
        GrabInput = new TriggerAction();
    }

    #region Move

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        var rawMoveInput = context.ReadValue<Vector2>();

        NormalizeMoveInputX(rawMoveInput.x);
        NormalizeMoveInputY(rawMoveInput.y);
    }

    private void NormalizeMoveInputX(Single rawMoveInputX) => NormInputX.Value = (Mathf.Abs(rawMoveInputX) > m_Data.moveInputTolerance) ? (Int32)(rawMoveInputX * Vector2.right).normalized.x : 0;
    private void NormalizeMoveInputY(Single rawMoveInputY) => NormInputY.Value = (Mathf.Abs(rawMoveInputY) > m_Data.moveInputTolerance) ? (Int32)(rawMoveInputY * Vector2.up).normalized.y : 0;

    #endregion

    #region Jump

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput.Initiate();
            JumpInputHold.Initiate();
        }
        else if (context.canceled)
        {
            JumpInputHold.Initiate();
        }
    }

    #endregion

    #region Wall Climb/Grab

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabInput.Initiate();
        }
        else if (context.canceled)
        {
            GrabInput.Terminate();
        }
    }

    #endregion
}
