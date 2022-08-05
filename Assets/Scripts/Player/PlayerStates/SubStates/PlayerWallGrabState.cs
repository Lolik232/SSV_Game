using System;
using UnityEngine;
public class PlayerWallGrabState : PlayerTouchingWallState
{

    private Vector2 _holdPosition;
    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
    }

    public override void Enter()
    {
        base.Enter();

        _holdPosition = _player.Rigidbody.position;

        HoldPosition();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        HoldPosition();

        if (!_isExitingState)
        {
            if (_yInput > 0f)
            {
                _stateMachine.ChangeState(_player.WallClimbState);
            }
            else if (_yInput < 0f || !_grabInput)
            {
                _stateMachine.ChangeState(_player.WallSlideState);
            }
        }
    }

    private void HoldPosition()
    {
        _player.Rigidbody.position = _holdPosition;

        _player.SetVelocityX(0f);
        _player.SetVelocityY(0f);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
