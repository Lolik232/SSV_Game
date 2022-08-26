using System;

using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 m_HoldPosition;

    private Single m_EnduranceGrabLimit;

    public PlayerWallGrabState(PlayerStatesManager statesManager,string animBoolName) : base(statesManager, animBoolName)
    {
        m_EnduranceGrabLimit = Data.enduranceGrabLimit;
    }

    public override void Enter()
    {
        base.Enter();

        m_HoldPosition = MoveController.Transform.position;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        HoldPosition();

        if (InputY > 0f)
        {
            StateMachine.ChangeState(StatesManager.WallClimbState);
        } 
        else if (InputY < 0f || !GrabInput)
        {
            StateMachine.ChangeState(StatesManager.WallSlideState);
        }
    }

    private void HoldPosition()
    {
        MoveController.Transform.position = m_HoldPosition;

        MoveController.SetVelocityZero();
    }
}
