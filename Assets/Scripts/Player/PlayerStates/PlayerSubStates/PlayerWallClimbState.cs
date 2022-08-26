using System;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    private Single m_EnduranceClimbLimit;

    public PlayerWallClimbState(PlayerStatesManager statesManager, string animBoolName) : base(statesManager, animBoolName)
    {
        m_EnduranceClimbLimit = Data.enduranceClimbLimit;
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

        MoveController.SetVelocityY(Data.wallClimbVelocity);

        if (InputY != 1)
        {
            StateMachine.ChangeState(StatesManager.WallGrabState);
        }
    }
}
