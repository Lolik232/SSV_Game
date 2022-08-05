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
        
        if (_isExitingState)
        {
            return;
        }

        _player.SetVelocityY(_playerData.wallClimbVelocity);

        if (_yInput != 1)
        {
            _stateMachine.ChangeState(_player.WallGrabState);
        }
    }
}
