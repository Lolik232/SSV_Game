using System;

using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerStatesManager statesManager, string animBoolName) : base(statesManager, animBoolName)
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

        if (InputX == 0)
        {
            StateMachine.ChangeState(StatesManager.IdleState);
        }
        else
        {
            SendMove(Data.movementVelocity, InputX);
        }
    }
}
