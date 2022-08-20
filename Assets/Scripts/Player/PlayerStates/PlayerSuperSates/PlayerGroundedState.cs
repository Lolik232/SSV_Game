using System;
using UnityEngine;

public class PlayerGroundedState : PlayerEnvironmentState
{
    public PlayerGroundedState(Player player, PlayerStatesDescriptor statesDescriptor, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, statesDescriptor, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger(int id = 0)
    {
        base.AnimationTrigger(id);
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        StatesDescriptor.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (JumpInput && StatesDescriptor.JumpState.CanJump())
        {
            StateMachine.ChangeState(StatesDescriptor.JumpState);
        }
        else if (!IsGrounded)
        {
            StatesDescriptor.InAirState.StartCoyoteTime();
            ChangeState(StatesDescriptor.InAirState);
        } 
        else if (IsTouchingWall && IsTouchingLedge && GrabInput)
        {
            ChangeState(StatesDescriptor.WallGrabState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
