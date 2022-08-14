using System;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    private Boolean _isGrounded;
    public PlayerAbilityState(Player player, PlayerStatesDescriptor statesDescriptor, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, statesDescriptor, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger(int id = 0)
    {
        base.AnimationTrigger(id);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = _player.CheckIfGrounded();
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

        if (!_isActive)
        {
            if (_isGrounded && _player.CurrentVelocity.y < _data.groundSlopeTolerance)
            {
                ChangeState(_statesDescriptor.IdleState);
            }
            else
            {
                ChangeState(_statesDescriptor.InAirState);
            }
        } 
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
