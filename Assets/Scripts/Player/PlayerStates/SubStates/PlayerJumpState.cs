using System;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private Int32 _amountOfJumpsLeft;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        _amountOfJumpsLeft = _playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        _player.InputHandler.UseJumpInput();

        _player.SetVelocityY(_playerData.jumpVelocity);
        _isAbilityDone = true;

        _amountOfJumpsLeft--;

        _player.InAirState.SetIsJumping();
    }

    public Boolean CanJump() => _amountOfJumpsLeft > 0;

    public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = _playerData.amountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;

    public void EmptyAmountOfJumpsLeft() => _amountOfJumpsLeft = 0;
}
