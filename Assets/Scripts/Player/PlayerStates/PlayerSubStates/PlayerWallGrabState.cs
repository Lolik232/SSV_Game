using System;

using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 m_HoldPosition;

    private Single m_EnduranceGrabLimit;

    public PlayerWallGrabState(Player player, PlayerStatesDescriptor statesDescriptor, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, statesDescriptor, stateMachine, playerData, animBoolName)
    {
        m_EnduranceGrabLimit = Data.enduranceGrabLimit;
    }

    public override void Enter()
    {
        base.Enter();

        m_HoldPosition = Player.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        HoldPosition();

        if (StatesDescriptor.WallClimbState.CanClimb() && InputY > 0f)
        {
            ChangeState(StatesDescriptor.WallClimbState);
        } 
        else if (Player.Endurance.IsEmpty() || InputY < 0f || !GrabInput)
        {
            ChangeState(StatesDescriptor.WallSlideState);
        }

        DecreaseEndurance();
    }

    private void HoldPosition()
    {
        Player.transform.position = m_HoldPosition;

        Player.ResetVelocity();
    }
    public Boolean CanGrab() => Player.Endurance.CurrentPoints >= m_EnduranceGrabLimit;

    private void DecreaseEndurance()
    {
        Player.Endurance.ChangePoints(-Data.grabEnduranceDecreasing * Time.deltaTime);
    }
}
