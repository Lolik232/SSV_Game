using System;
using UnityEngine;

public class PlayerInAirState : PlayerEnvironmentState
{
    public PlayerInAirState(Player player, PlayerStatesDescriptor statesDescriptor, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, statesDescriptor, stateMachine, playerData, animBoolName)
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

        if (_isGrounded && _player.CurrentVelocity.y < _data.groundSlopeTolerance)
        {
            ChangeState(_statesDescriptor.LandState);
        } 
        else
        {
            _player.CheckIfShouldFlip(_xInput);

            _player.SetVelocityX(_data.movementVelocity * _xInput);

            _player.Animator.SetFloat("yVelocity", _player.CurrentVelocity.y);
            _player.Animator.SetFloat("xVelocity", Mathf.Abs(_player.CurrentVelocity.x));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
