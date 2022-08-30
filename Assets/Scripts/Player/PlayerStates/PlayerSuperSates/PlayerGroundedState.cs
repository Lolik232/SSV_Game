using System;

using UnityEngine;

public abstract class PlayerGroundedState : PlayerEnvironmentState
{
    protected PlayerGroundedState(PlayerStatesManager statesManager, Player player, PlayerData data, string animBoolName) : base(statesManager, player, data, animBoolName)
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsActive) { return; }

        if (JumpInput && Player.AbilitiesManager.JumpAbility.CanJump)
        {
            StatesManager.StateMachine.ChangeState(StatesManager.JumpState);
        }
        else if (!IsGrounded)
        {
            StatesManager.InAirState.CoyoteTime.Initiate();
            StatesManager.StateMachine.ChangeState(StatesManager.InAirState);
        }
        else if (IsTouchingWall && IsTouchingLedge && GrabInput && Player.AbilitiesManager.WallClimbAbility.CanGrab)
        {
            StatesManager.StateMachine.ChangeState(StatesManager.WallGrabState);
        }
    }
}
