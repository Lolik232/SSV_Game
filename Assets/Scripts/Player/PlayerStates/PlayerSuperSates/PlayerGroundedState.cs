using System;

using UnityEngine;

public class PlayerGroundedState : PlayerEnvironmentState
{

    public PlayerGroundedState(PlayerStatesManager statesManager, string animBoolName) : base(statesManager, animBoolName)
    {
    }

    public event Action GroundLeaveEvent;

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

        if (JumpInput && AbilitiesManager.JumpAbility.CanJump)
        {
            SendGroundLeave();
            StateMachine.ChangeState(StatesManager.JumpState);
        }
        else if (!IsGrounded)
        {
            SendGroundLeave();
            StateMachine.ChangeState(StatesManager.InAirState);
        }
        else if (IsTouchingWall && IsTouchingLedge && GrabInput)
        {
            SendGroundLeave();
            StateMachine.ChangeState(StatesManager.WallGrabState);
        }
    }

    protected void SendGroundLeave()
    {
        GroundLeaveEvent?.Invoke();
    }
}
