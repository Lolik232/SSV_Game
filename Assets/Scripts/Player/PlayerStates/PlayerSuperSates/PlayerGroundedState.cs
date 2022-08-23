using System;
using UnityEngine;

public class PlayerGroundedState : PlayerEnvironmentState
{
    public PlayerGroundedState(PlayerStatesManager statesDescriptor, string animBoolName) : base(statesDescriptor, animBoolName)
    {
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
            StateMachine.ChangeState(StatesDescriptor.InAirState);
        } 
        else if (IsTouchingWall && IsTouchingLedge && GrabInput)
        {
            StateMachine.ChangeState(StatesDescriptor.WallGrabState);
        }
    }
}
