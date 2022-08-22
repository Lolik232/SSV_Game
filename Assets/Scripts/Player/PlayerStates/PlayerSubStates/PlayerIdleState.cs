using System;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStatesManager statesDescriptor, string animBoolName) : base(statesDescriptor, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        Player.ResetVelocityX();
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
            ChangeState(StatesDescriptor.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {

        base.PhysicsUpdate();
    }
}
