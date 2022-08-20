using System;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    private Single m_EnduranceClimbLimit;

    public PlayerWallClimbState(Player player, PlayerStatesDescriptor statesDescriptor, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, statesDescriptor, stateMachine, playerData, animBoolName)
    {
        m_EnduranceClimbLimit = Data.enduranceClimbLimit;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Player.SetVelocityY(Data.wallClimbVelocity);

        if (!StatesDescriptor.WallGrabState.CanGrab() || InputY != 1)
        {
            ChangeState(StatesDescriptor.WallGrabState);
        }

        DecreaseEndurance();
    }

    public Boolean CanClimb() => Player.Endurance.CurrentPoints >= m_EnduranceClimbLimit;

    private void DecreaseEndurance()
    {
        Player.Endurance.ChangePoints(-Data.climbEnduranceDecreasing * Time.deltaTime);
    }
}
