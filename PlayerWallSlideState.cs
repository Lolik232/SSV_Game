using System;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStatesDescriptor statesDescriptor, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, statesDescriptor, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Grab
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Player.SetVelocityY(-Data.wallSlideVelocity);

        if (StatesDescriptor.WallGrabState.CanGrab() && GrabInput && InputY == 0)
        {
            ChangeState(StatesDescriptor.WallGrabState);
        }
    }
}
