using System;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isExitingState)
        {
            return;
        } 

        _player.SetVelocityY(-_playerData.wallSlideVelocity);

        if (_grabInput && _yInput == 0)
        {
            _stateMachine.ChangeState(_player.WallGrabState);
        }
    }
}
