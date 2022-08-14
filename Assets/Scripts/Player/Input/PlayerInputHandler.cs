using System;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerData _data;

    public Int32 NormInputX { get; private set; }
    public Int32 NormInputY { get; private set; }

    public Boolean JumpInput { get; private set; }
    public Boolean IsJumpInputActive { get; private set; }

    [SerializeField]
    private Single _jumpInputHoldTime = 0.1f;

    private Single _jumpInputStartTime;

    private Single JumpEndTime { get => _jumpInputStartTime + _jumpInputHoldTime; }


    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void Initialize(PlayerData data)
    {
        _data = data;   
    }

    #region Move

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        var rawMoveInput = context.ReadValue<Vector2>();

        NormalizeMoveInputX(rawMoveInput.x);
        NormalizeMoveInputY(rawMoveInput.y);
    }

    private void NormalizeMoveInputX(Single rawMoveInputX) => NormInputX = (Mathf.Abs(rawMoveInputX) > _data.moveInputTolerance) ? (Int32)(rawMoveInputX * Vector2.right).normalized.x : 0;
    private void NormalizeMoveInputY(Single rawMoveInputY) => NormInputY = (Mathf.Abs(rawMoveInputY) > _data.moveInputTolerance) ? (Int32)(rawMoveInputY * Vector2.up).normalized.y : 0;

    #endregion

    #region Jump

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            IsJumpInputActive = true;
            _jumpInputStartTime = GetScaledTime();
        }
        else if (context.canceled)
        {
            IsJumpInputActive = false;
        }
    }

    public void MarkJumpInputAsUsed() => JumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if (GetScaledTime() >= JumpEndTime)
        {
            JumpInput = false;
        }
    }

    #endregion

    public Single GetScaledTime()
    {
        return Time.time;
    }

    public Single GetUnscaledTime()
    {
        return Time.unscaledTime;
    }
}
