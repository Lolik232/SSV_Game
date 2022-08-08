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

        _holdPosition = _player.transform.position;

        HoldPosition();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isExitingState)
        {
            return;
        }

        HoldPosition();

        if (_yInput > 0f)
        {
            _stateMachine.ChangeState(_player.WallClimbState);
        }
        else if (_yInput < 0f || !_grabInput)
        {
            _stateMachine.ChangeState(_player.WallSlideState);
        }
    }

    private void HoldPosition()
    {
        _player.transform.position = _holdPosition;

        _player.SetVelocityZero();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
