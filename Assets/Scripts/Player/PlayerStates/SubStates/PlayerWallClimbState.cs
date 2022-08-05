using System;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _player.SetVelocityY(_playerData.wallClimbVelocity);

        if (_yInput != 1 && !_isExitingState)
        {
            _stateMachine.ChangeState(_player.WallGrabState);
        }
    }
}
