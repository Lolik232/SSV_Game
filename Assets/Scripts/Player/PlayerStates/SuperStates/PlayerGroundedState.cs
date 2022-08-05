using System;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected Int32 _xInput;

    private Boolean _jumpInput;

    private Boolean _isGrounded;

    private Boolean _isTouchingWall;

    private Boolean _grabInput;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, String animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

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

        _player.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _xInput = _player.InputHandler.NormInutX;
        _jumpInput = _player.InputHandler.JumpInput;
        _grabInput = _player.InputHandler.GrabInput;

        if (_jumpInput && _player.JumpState.CanJump())
        {
            _player.InputHandler.UseJumpInput();
            _stateMachine.ChangeState(_player.JumpState);
        }
        else if (!_isGrounded)
        {
            _player.InAirState.StartCoyoteTime();
            _stateMachine.ChangeState(_player.InAirState);
        }
        else if (_isTouchingWall && _grabInput)
        {
            _stateMachine.ChangeState(_player.WallGrabState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
