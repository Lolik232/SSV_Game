using System;

using UnityEngine;

public class PlayerInAirState : PlayerEnvironmentState
{
    private TriggerAction m_Jumping;

    private TimeDependentAction m_CoyoteTime;

    public PlayerInAirState(PlayerStatesManager statesManager, string animBoolName) : base(statesManager, animBoolName)
    {
        m_CoyoteTime = new TimeDependentAction(Data.jumpCoyoteTime);
        m_Jumping = new TriggerAction();
    }

    public override void Enter()
    {
        base.Enter();

        InputHandler.JumpInputHold.StateChangedEvent += OnJumpInputHoldStateChanged;
        MoveController.CurrentVelocityY.StateChangedEvent += OnCurrentVelocityYChanged;
        
        m_CoyoteTime.StateChangedEvent += OnCoyoteTimeStateChanged;
    }

    public override void Exit()
    {
        base.Exit();

        InputHandler.JumpInputHold.StateChangedEvent -= OnJumpInputHoldStateChanged;
        MoveController.CurrentVelocityY.StateChangedEvent -= OnCurrentVelocityYChanged;
        m_CoyoteTime.StateChangedEvent -= OnCoyoteTimeStateChanged;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsGrounded && MoveController.CurrentVelocityY < Data.groundSlopeTolerance)
        {
            StateMachine.ChangeState(StatesManager.LandState);
        }
        else if (JumpInput && StatesManager.JumpState.CanJump())
        {
            InputHandler.JumpInput.Terminate();
            StateMachine.ChangeState(StatesManager.JumpState);
        }
        else if (IsTouchingWall && IsTouchingLedge && GrabInput)
        {
            StateMachine.ChangeState(StatesManager.WallGrabState);
        }
        else if (IsTouchingWall && InputX == MoveController.FacingDirection && MoveController.CurrentVelocityY <= 0f)
        {
            StateMachine.ChangeState(StatesManager.WallSlideState);
        }
        else
        {
            MoveController.CheckIfShouldFlip(InputX);
            MoveController.SetVelocityX(Data.movementVelocity * InputX);
        }
    }

    private void OnPlayerJumpStateChanged(Boolean isActive)
    {
        if (isActive)
        {
            m_Jumping.Initiate();
        }
    }

    public void StartCoyoteTime() => m_CoyoteTime.Initiate();

    private void OnJumpInputHoldStateChanged(Boolean isActive)
    {
        if (m_Jumping.IsActive && !isActive && MoveController.CurrentVelocityY > 0f)
        {
            MoveController.SetVelocityY(MoveController.CurrentVelocityY * Data.variableJumpHeightMultiplier);
            m_Jumping.Terminate();
        }
    }

    private void OnCurrentVelocityYChanged(Single value)
    {
        if (m_Jumping.IsActive && value <= 0f)
        {
            m_Jumping.Terminate();
        }
    }

    private void OnCoyoteTimeStateChanged(Boolean isActive)
    {
        if (isActive)
        {
            StatesManager.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }
}
