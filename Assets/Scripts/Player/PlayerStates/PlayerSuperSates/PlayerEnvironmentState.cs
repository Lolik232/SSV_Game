using System;

using UnityEngine;

public abstract class PlayerEnvironmentState : PlayerState
{
    protected bool IsGrounded;
    protected bool IsGroundClose;
    protected bool IsTouchingWall;
    protected bool IsTouchingWallBack;
    protected bool IsTouchingLedge;

    protected bool JumpInput;
    protected bool GrabInput;

    protected PlayerEnvironmentState(PlayerStatesManager statesManager, Player player, PlayerData data, string animBoolName) : base(statesManager, player, data, animBoolName)
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

    public override void InputUpdate()
    {
        base.InputUpdate();

        JumpInput = Player.InputHandler.JumpInput;
        GrabInput = Player.InputHandler.GrabInput;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsActive) { return; }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected override void DoChecks()
    {
        base.DoChecks();

        IsGrounded = Player.EnvironmentCheckersManager.GroundChecker.IsDetected;
        IsGroundClose = Player.EnvironmentCheckersManager.GroundCloseChecker.IsDetected;
        IsTouchingWall = Player.EnvironmentCheckersManager.WallChecker.IsDetected;
        IsTouchingWallBack = Player.EnvironmentCheckersManager.WallBackChecker.IsDetected;
        IsTouchingLedge = Player.EnvironmentCheckersManager.LedgeChecker.IsDetected;
    }
}
