using System;
using UnityEngine;

public class PlayerEnvironmentState : PlayerState
{
    protected Boolean IsGrounded;
    protected Boolean IsGroundFar;
    protected Boolean IsTouchingWall;
    protected Boolean IsTouchingWallBack;
    protected Boolean IsTouchingLedge;

    protected Boolean JumpInput;
    protected Boolean GrabInput;

    public PlayerEnvironmentState(Player player, PlayerStatesManager statesDescriptor, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, statesDescriptor, stateMachine, playerData, animBoolName)
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

        IsGrounded = Player.CheckIfGrounded();
        IsGroundFar = Player.CheckIfGroundFar();
        IsTouchingWall = Player.CheckIfTouchingWall();
        IsTouchingWallBack = Player.CheckIfTouchingWallBack();
        IsTouchingLedge = Player.CheckIfTouchingLedge();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        JumpInput = Player.InputHandler.JumpInput.IsActive;
        GrabInput = Player.InputHandler.GrabInput.IsActive;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
