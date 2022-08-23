using System;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerStatesManager statesDescriptor, string animBoolName) : base(statesDescriptor, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
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

        MoveController.CheckIfShouldFlip(InputX);
        MoveController.SetVelocityX(Data.movementVelocity * InputX);

        if (InputX == 0)
        {
            StateMachine.ChangeState(StatesDescriptor.IdleState);
        } 
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
