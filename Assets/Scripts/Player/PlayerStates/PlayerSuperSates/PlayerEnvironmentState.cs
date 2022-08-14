using System;
using UnityEngine;

public class PlayerEnvironmentState : PlayerState
{
    protected Boolean _isGrounded;
    protected Boolean _isGroundFar;
    protected Boolean _isTouchingWall;
    protected Boolean _isTouchingWallBack;
    protected Boolean _isTouchingLedge;

    protected Boolean _jumpInput;

    public PlayerEnvironmentState(Player player, PlayerStatesDescriptor statesDescriptor, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, statesDescriptor, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger(int id = 0)
    {
        base.AnimationTrigger(id);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = _player.CheckIfGrounded();
        _isGroundFar = _player.CheckIfGroundFar();
        _isTouchingWall = _player.CheckIfTouchingWall();
        _isTouchingWallBack = _player.CheckIfTouchingWallBack();
        _isTouchingLedge = _player.CheckIfTouchingLedge();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _jumpInput = _player.InputHandler.JumpInput;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
