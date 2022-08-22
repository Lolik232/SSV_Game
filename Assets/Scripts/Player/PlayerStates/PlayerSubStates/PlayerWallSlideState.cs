using System;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStatesManager statesDescriptor, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, statesDescriptor, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Player.SetVelocityY(-Data.wallSlideVelocity);

        if (StatesDescriptor.WallGrabState.CanGrab() && GrabInput && InputY >= 0)
        {
            ChangeState(StatesDescriptor.WallGrabState);
        }
    }
}
