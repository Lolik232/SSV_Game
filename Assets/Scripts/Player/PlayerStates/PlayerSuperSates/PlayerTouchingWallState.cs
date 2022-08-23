using System;
using UnityEngine;

public class PlayerTouchingWallState : PlayerEnvironmentState
{
    public PlayerTouchingWallState(PlayerStatesManager statesManager, string animBoolName) : base(statesManager, animBoolName)
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

        if (JumpInput)
        {

        }
        else if (IsGrounded && !GrabInput)
        {
            StateMachine.ChangeState(StatesManager.IdleState);
        }
        else if (!IsTouchingWall || (InputX != MoveController.FacingDirection && !GrabInput))
        {
            StateMachine.ChangeState(StatesManager.InAirState);
        }
    }
}
