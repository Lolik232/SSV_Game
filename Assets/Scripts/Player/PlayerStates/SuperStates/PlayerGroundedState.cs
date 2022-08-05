using System;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected Int32 _xInput;

    private Boolean _jumpInput;

    private Boolean _isGrounded;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, String animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = _player.CheckIfGrounded();
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
