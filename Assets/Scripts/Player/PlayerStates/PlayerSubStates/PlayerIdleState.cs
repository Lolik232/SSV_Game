using System;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStatesDescriptor statesDescriptor, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, statesDescriptor, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        _player.ResetVelocityX();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_xInput != 0)
        {
            ChangeState(_statesDescriptor.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {

        base.PhysicsUpdate();
    }
}
