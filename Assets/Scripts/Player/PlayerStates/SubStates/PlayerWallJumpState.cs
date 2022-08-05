using System;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private Int32 _wallJumpDirection;
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _player.JumpState.ResetAmountOfJumpsLeft();
        _player.SetVelocity(_playerData.wallJumpVelocity, _playerData.wallJumpAngle, _wallJumpDirection);
        _player.CheckIfShouldFlip(_wallJumpDirection);
        _player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _player.Anim.SetFloat("yVelocity", _player.CurrentVelocity.y);
        _player.Anim.SetFloat("xVelocity", Mathf.Abs(_player.CurrentVelocity.x));

        if (Time.time >= _startTime + _playerData.wallJumpTime)
        {
            _isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(Boolean isTouchingWall)
    {
        _wallJumpDirection = isTouchingWall ? -_player.FacingDirection : _player.FacingDirection;
    }
}
