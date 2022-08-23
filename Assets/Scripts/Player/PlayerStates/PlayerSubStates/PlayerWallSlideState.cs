using System;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(PlayerStatesManager statesManager, string animBoolName) : base(statesManager,  animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        MoveController.SetVelocityY(-Data.wallSlideVelocity);

        if (GrabInput && InputY >= 0)
        {
            StateMachine.ChangeState(StatesManager.WallGrabState);
        }
    }
}
