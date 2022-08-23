using System;

using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]  private PlayerData m_Data;
    
    public Int32 NormInputX { get; private set; }
    public Int32 NormInputY { get; private set; }

    public TimeDependentState JumpInput { get; private set; }

    public Boolean IsJumpInputHold { get; private set; }

    public TriggerState GrabInput { get; private set; }


    private void Awake()
    {
        JumpInput = new TimeDependentState(m_Data.jumpInputHoldTime);
        GrabInput = new TriggerState();
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    #region Move

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        var rawMoveInput = context.ReadValue<Vector2>();

        NormalizeMoveInputX(rawMoveInput.x);
        NormalizeMoveInputY(rawMoveInput.y);
    }

    private void NormalizeMoveInputX(Single rawMoveInputX) => NormInputX = (Mathf.Abs(rawMoveInputX) > m_Data.moveInputTolerance) ? (Int32)(rawMoveInputX * Vector2.right).normalized.x : 0;
    private void NormalizeMoveInputY(Single rawMoveInputY) => NormInputY = (Mathf.Abs(rawMoveInputY) > m_Data.moveInputTolerance) ? (Int32)(rawMoveInputY * Vector2.up).normalized.y : 0;

    #endregion

    #region Jump

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput.Initiate();
            IsJumpInputHold = true;
        }
        else if (context.canceled)
        {
            IsJumpInputHold = false;
        }
    }

    private void CheckJumpInputHoldTime()
    {
        if (JumpInput.IsOutOfTime())
        {
            JumpInput.Terminate();
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
