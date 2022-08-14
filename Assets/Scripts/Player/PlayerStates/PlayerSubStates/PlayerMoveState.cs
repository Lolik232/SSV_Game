using System;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStatesDescriptor statesDescriptor, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, statesDescriptor, stateMachine, playerData, animBoolName)
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

        _player.CheckIfShouldFlip(_xInput);
        _player.SetVelocityX(_data.movementVelocity * _xInput);

        if (_xInput == 0)
        {
            _stateMachine.ChangeState(_statesDescriptor.IdleState);
        } 
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
