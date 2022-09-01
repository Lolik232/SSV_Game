using System;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerData m_Data;

    public ValueChangingAction<int> NormInputX { get; private set; }
    public ValueChangingAction<int> NormInputY { get; private set; }

    public TimeDependentAction JumpInput { get; private set; }

    public TriggerAction JumpInputHold { get; private set; }

    public TriggerAction GrabInput { get; private set; }

    public Player Player { get; private set; }

    private void Awake()
    {
        Player = GetComponent<Player>();

        NormInputX = new ValueChangingAction<int>();
        NormInputY = new ValueChangingAction<int>();

        JumpInput = new TimeDependentAction(m_Data.jumpInputHoldTime);
        GrabInput = new TriggerAction();
        JumpInputHold = new TriggerAction();
    }

    public void Start()
    {
        Player.StatesManager.JumpState.EnterEvent += JumpInput.Terminate;
        Player.StatesManager.WallJumpState.EnterEvent += JumpInput.Terminate;
    }

    public void OnDestroy()
    {
        Player.StatesManager.JumpState.EnterEvent -= JumpInput.Terminate;
        Player.StatesManager.WallJumpState.EnterEvent -= JumpInput.Terminate;

    }

    #region Move

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        var rawMoveInput = context.ReadValue<Vector2>();

        NormalizeMoveInputX(rawMoveInput.x);
        NormalizeMoveInputY(rawMoveInput.y);
    }

    private void NormalizeMoveInputX(float rawMoveInputX) => NormInputX.Value = (Mathf.Abs(rawMoveInputX) > m_Data.moveInputTolerance) ? (int)(rawMoveInputX * Vector2.right).normalized.x : 0;
    private void NormalizeMoveInputY(float rawMoveInputY) => NormInputY.Value = (Mathf.Abs(rawMoveInputY) > m_Data.moveInputTolerance) ? (int)(rawMoveInputY * Vector2.up).normalized.y : 0;

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
            JumpInputHold.Terminate();
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
