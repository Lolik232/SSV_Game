using System;
using UnityEngine;

public class PlayerGroundedState : PlayerEnvironmentState
{
    public PlayerGroundedState(PlayerStatesManager statesManager, string animBoolName) : base(statesManager, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StatesManager.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (JumpInput && StatesManager.JumpState.CanJump())
        {
            StateMachine.ChangeState(StatesManager.JumpState);
        }
        else if (!IsGrounded)
        {
            StatesManager.InAirState.StartCoyoteTime();
            StateMachine.ChangeState(StatesManager.InAirState);
        } 
        else if (IsTouchingWall && IsTouchingLedge && GrabInput)
        {
            StateMachine.ChangeState(StatesManager.WallGrabState);
        }
    }
}
