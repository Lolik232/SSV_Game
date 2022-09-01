using System;
using UnityEngine;

public class PlayerTouchingWallState : PlayerEnvironmentState
{
    public PlayerTouchingWallState(PlayerStatesManager statesManager, Player player, PlayerData data, string animBoolName) : base(statesManager, player, data, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void InputUpdate()
    {
        base.InputUpdate();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (JumpInput && Player.AbilitiesManager.JumpAbility.CanWallJump)
        {
            StatesManager.StateMachine.ChangeState(StatesManager.WallJumpState);
        }
        else if (IsGrounded && !GrabInput)
        {
            StatesManager.StateMachine.ChangeState(StatesManager.IdleState);
        }
        else if (!IsTouchingWall || (InputX != Player.MoveController.FacingDirection && !GrabInput))
        {
            StatesManager.InAirState.WallJumpCoyoteTime.Initiate();
            StatesManager.StateMachine.ChangeState(StatesManager.InAirState);
        }
        else if (IsTouchingWall && !IsTouchingLedge && !IsGroundClose)
        {
            StatesManager.StateMachine.ChangeState(StatesManager.LedgeClimbState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected override void DoChecks()
    {
        base.DoChecks();
    }
}
