using System;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{

    private Vector2 _detectedPosition;
    private Vector2 _cornerPosition;

    private Vector2 _startPosition;
    private Vector2 _endPosition;

    private Boolean _isHanging;
    private Boolean _isClimbing;

    private Boolean _jumpInput;

    private Int32 _xInput;
    private Int32 _yInput;
    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        _isHanging = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        _player.SetVelocityZero();
        _player.transform.position = _detectedPosition;
        _cornerPosition = _player.DetermineCornerPosition();

        Single startX = _cornerPosition.x - _player.FacingDirection * _playerData.startOffset.x;
        Single startY = _cornerPosition.y - _playerData.startOffset.y;
        Single endX = _cornerPosition.x + _player.FacingDirection * _playerData.endOffset.x;
        Single endY = _cornerPosition.y + _playerData.endOffset.y;

        _startPosition.Set(startX, startY);
        _endPosition.Set(endX, endY);

        _player.transform.position = _startPosition;
    }

    public override void Exit()
    {
        base.Exit();

        if (_isClimbing)
        {
            _player.transform.position = _endPosition;
        }

        _player.Anim.SetBool("isClimbingLedge", false);

        _isHanging = false;
        _isClimbing = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isExitingState)
        {
            return;
        }

        if (_isAnimationFinished)
        {
            _stateMachine.ChangeState(_player.IdleState);

            return;
        }   

        _xInput = _player.InputHandler.NormInutX;
        _yInput = _player.InputHandler.NormInutY;
        _jumpInput = _player.InputHandler.JumpInput;

        _player.SetVelocityZero();
        _player.transform.position = _startPosition;

        if (_xInput == _player.FacingDirection && _isHanging && !_isClimbing)
        {
            _isClimbing = true;

            _player.Anim.SetBool("isClimbingLedge", true);
        }
        else if (_yInput == -1 && _isHanging && !_isClimbing)
        {
            _stateMachine.ChangeState(_player.InAirState);
        } 
        else if ( _jumpInput && !_isClimbing) 
        {
            _player.WallJumpState.DetermineWallJumpDirection(true);
            _stateMachine.ChangeState(_player.WallJumpState);
        }
    }

    public void SetDetectedPosition(Vector2 pos) => _detectedPosition = pos;
}
