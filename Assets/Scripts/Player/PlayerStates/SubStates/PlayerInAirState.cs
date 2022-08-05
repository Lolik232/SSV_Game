using System;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private Int32 _xInput;

    private Boolean _isGrounded;

    private Boolean _jumpInput;
    private Boolean _jumpInputStop;

    private Boolean _coyoteTime;
    private Boolean _wallJumpCoyoteTime;

    private Boolean _isJumping;

    private Boolean _isTouchingWall;
    private Boolean _isTouchingWallBack;

    private Boolean _oldIsTouchingWall;
    private Boolean _oldIsTouchingWallBack;

    private Boolean _grabInput;

    private Single _startWallJumpCoyoteTime;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _oldIsTouchingWall = _isTouchingWall;
        _oldIsTouchingWallBack = _isTouchingWallBack;

        _isGrounded = _player.CheckIfGrounded();
        _isTouchingWall = _player.CheckIftTouchingWall();
        _isTouchingWallBack = _player.CheckIftouchingWallBack();

        if (!_wallJumpCoyoteTime && !_isTouchingWall && !_isTouchingWallBack && (_oldIsTouchingWall || _oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        _oldIsTouchingWall = false;
        _oldIsTouchingWallBack = false;
        _isTouchingWall = false;
        _isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        _xInput = _player.InputHandler.NormInutX;

        _jumpInput = _player.InputHandler.JumpInput;
        _jumpInputStop = _player.InputHandler.JumpInputStop;
        _grabInput = _player.InputHandler.GrabInput;

        CheckJumpMultiplier();

        if (_isGrounded && _player.CurrentVelocity.y < 0.01f)
        {
            _stateMachine.ChangeState(_player.LandState);
        } 
        else if (_jumpInput && (_isTouchingWall || _isTouchingWallBack || _wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();

            _isTouchingWall = _player.CheckIftTouchingWall();
            _player.WallJumpState.DetermineWallJumpDirection(_isTouchingWall);

            _stateMachine.ChangeState(_player.WallJumpState);
        }
        else if (_jumpInput && _player.JumpState.CanJump())
        {
            _player.InputHandler.UseJumpInput();
            _stateMachine.ChangeState(_player.JumpState);
        }
        else if (_isTouchingWall && _grabInput)
        {
            _stateMachine.ChangeState(_player.WallGrabState);
        }
        else if (_isTouchingWall && _xInput == _player.FacingDirection && _player.CurrentVelocity.y <= 0f)
        {
            _stateMachine.ChangeState(_player.WallSlideState);
        }
        else
        {
            _player.CheckIfShouldFlip(_xInput);

            _player.SetVelocityX(_playerData.movementVelocity * _xInput);

            _player.Anim.SetFloat("yVelocity", _player.CurrentVelocity.y);
            _player.Anim.SetFloat("xVelocity", Mathf.Abs(_player.CurrentVelocity.x));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckJumpMultiplier()
    {
        if (_isJumping)
        {
            if (_jumpInputStop)
            {
                _player.SetVelocityY(_player.CurrentVelocity.y * _playerData.variableJumpHeightMultiplier);

                _isJumping = false;
            }
            else if (_player.CurrentVelocity.y <= 0)
            {
                _isJumping = false;
            }
        }
    }

    private void CheckCoyoteTime()
    {
        if (_coyoteTime && Time.time > _startTime + _playerData.coyoteTime)
        {
            _coyoteTime = false;

            _player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    private void CheckWallJumpCoyoteTime()
    {
        if (_wallJumpCoyoteTime && Time.time > _startWallJumpCoyoteTime + _playerData.coyoteTime)
        {
            _wallJumpCoyoteTime = false;
        }
    }

    public void StartCoyoteTime() => _coyoteTime = true;

    public void StartWallJumpCoyoteTime()
    {
        _wallJumpCoyoteTime = true;
        _startWallJumpCoyoteTime = Time.time;
    } 

    public void StopWallJumpCoyoteTime() => _wallJumpCoyoteTime = false;

    public void SetIsJumping() => _isJumping = true;
}
