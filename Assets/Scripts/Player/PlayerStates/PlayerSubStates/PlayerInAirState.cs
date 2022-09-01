using System;

using UnityEngine;

public class PlayerInAirState : PlayerEnvironmentState
{
    public TriggerAction Jumping { get; private set; }

    public TimeDependentAction JumpCoyoteTime { get; private set; }
    public TimeDependentAction WallJumpCoyoteTime { get; private set; }

    private bool m_IsJumpInputHold;

    public PlayerInAirState(PlayerStatesManager statesManager, Player player, PlayerData data, string animBoolName) : base(statesManager, player, data, animBoolName)
    {
        Jumping = new TriggerAction();
        JumpCoyoteTime = new TimeDependentAction(Data.jumpCoyoteTime);
        WallJumpCoyoteTime = new TimeDependentAction(Data.jumpCoyoteTime);
    }

    public event Action MoveEvent;

    public event Action JumpStopEvent;

    public override void Enter()
    {
        StatesManager.JumpState.IsGroundedBlock = false;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!IsActive) { return; }

        CheckIfJumpEnd();
        CheckJumpHold();

        if (IsGrounded && Velocity.y < Data.groundSlopeTolerance)
        {
            StatesManager.StateMachine.ChangeState(StatesManager.LandState);
        }
        else if (IsTouchingWall && !IsTouchingLedge && !IsGroundClose)
        {
            StatesManager.StateMachine.ChangeState(StatesManager.LedgeClimbState);
        }
        else if (JumpInput && (IsTouchingWall || IsTouchingWallBack || WallJumpCoyoteTime))
        {
            StatesManager.StateMachine.ChangeState(StatesManager.WallJumpState);
        }
        else if (JumpInput && Player.AbilitiesManager.JumpAbility.CanJump)
        {
            StatesManager.StateMachine.ChangeState(StatesManager.JumpState);
        }
        else if (Player.AbilitiesManager.WallClimbAbility.CanWallGrab && IsTouchingWall && IsTouchingLedge && GrabInput)
        {
            StatesManager.StateMachine.ChangeState(StatesManager.WallGrabState);
        }
        else if (IsTouchingWall && InputX == Player.MoveController.FacingDirection && Velocity.y <= 0f)
        {
            StatesManager.StateMachine.ChangeState(StatesManager.WallSlideState);
        }
        else
        {
            OnMove();
        }
    }

    public override void InputUpdate()
    {
        base.InputUpdate();
        m_IsJumpInputHold = Player.InputHandler.JumpInputHold;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected override void DoChecks()
    {
        base.DoChecks();
    }

    protected void OnJumpInputStop()
    {
        JumpStopEvent?.Invoke();
    }

    private void CheckJumpHold()
    {
        if (IsActive && Jumping && !m_IsJumpInputHold && Velocity.y > 0f)
        {
            OnJumpInputStop();
        }
    }

    private void CheckIfJumpEnd()
    {
        if (Jumping.IsActive && Velocity.y <= 0f)
        {
            Jumping.Terminate();
        }
    }

    protected virtual void OnMove()
    {
        MoveEvent?.Invoke();
    }
}
