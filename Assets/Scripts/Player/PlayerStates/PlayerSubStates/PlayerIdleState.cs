using System;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStatesManager statesManager, string animBoolName) : base(statesManager, animBoolName)
    {
    }

    public event Action StandEvent;

    public override void Enter()
    {
        base.Enter();
        SendStand();
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

    protected void SendStand()
    {
        StandEvent?.Invoke();
    }
}
