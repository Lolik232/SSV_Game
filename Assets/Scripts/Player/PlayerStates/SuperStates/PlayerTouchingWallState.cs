using System;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{

    protected Boolean _isGrounded;
    protected Boolean _isTouchingWall;

    protected Boolean _grabInput;

    protected Int32 _xInput;
    protected Int32 _yInput;
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
        _yInput = _player.InputHandler.NormInutY;
        _grabInput = _player.InputHandler.GrabInput;

        if (_isGrounded && !_grabInput)
        {
            _stateMachine.ChangeState(_player.IdleState);
        } 
        else if (!_isTouchingWall || (_xInput != _player.FacingDirection && !_grabInput))
        {
            _stateMachine.ChangeState(_player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
