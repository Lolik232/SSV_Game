using System;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{

    protected Boolean _isGrounded;
    protected Boolean _isTouchingWall;
    protected Int32 _xInput;
    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = _player.CheckIfGrounded();
        _isTouchingWall = _player.CheckIftouchingWall();
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

        _xInput = _player.InputHandler.NormInutX;

        if (_isGrounded)
        {
            _stateMachine.ChangeState(_player.IdleState);
        } 
        else if (!_isTouchingWall || _xInput != _player.FacingDirection)
        {
            _stateMachine.ChangeState(_player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
