using System;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStatesManager statesManager, string animBoolName) : base(statesManager, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        MoveController.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (InputX != 0)
        {
            StateMachine.ChangeState(StatesManager.MoveState);
        }
    }
}
