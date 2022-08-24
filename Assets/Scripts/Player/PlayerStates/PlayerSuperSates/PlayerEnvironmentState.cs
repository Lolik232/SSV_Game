using System;

using UnityEngine;

public class PlayerEnvironmentState : PlayerState
{
    protected Boolean IsGrounded;
    protected Boolean IsGroundClose;
    protected Boolean IsTouchingWall;
    protected Boolean IsTouchingWallBack;
    protected Boolean IsTouchingLedge;

    protected Boolean JumpInput;
    protected Boolean GrabInput;

    public PlayerEnvironmentState(PlayerStatesManager statesManager, string animBoolName) : base(statesManager, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        IsGrounded = EnvironmentCheckersManager.GroundChecker.IsDetected;
        IsGroundClose = EnvironmentCheckersManager.GroundCloseChecker.IsDetected;
        IsTouchingWall = EnvironmentCheckersManager.WallChecker.IsDetected;
        IsTouchingWallBack = EnvironmentCheckersManager.WallBackChecker.IsDetected;
        IsTouchingLedge = EnvironmentCheckersManager.LedgeChecker.IsDetected;

        EnvironmentCheckersManager.GroundChecker.TargetDetectionChangedEvent += SetIsGrounded;
        EnvironmentCheckersManager.GroundCloseChecker.TargetDetectionChangedEvent += SetIsGroundClose;
        EnvironmentCheckersManager.WallChecker.TargetDetectionChangedEvent += SetIsTouchingWall;
        EnvironmentCheckersManager.WallBackChecker.TargetDetectionChangedEvent += SetIsTouchingWallBack;
        EnvironmentCheckersManager.LedgeChecker.TargetDetectionChangedEvent += SetIsTouchingLedge;

        JumpInput = InputHandler.JumpInput;
        GrabInput = InputHandler.GrabInput;

        InputHandler.JumpInput.StateChangedEvent += SetJumpInput;
        InputHandler.GrabInput.StateChangedEvent += SetGrabInput;
    }

    public override void Exit()
    {
        base.Exit();

        EnvironmentCheckersManager.GroundChecker.TargetDetectionChangedEvent -= SetIsGrounded;
        EnvironmentCheckersManager.GroundCloseChecker.TargetDetectionChangedEvent -= SetIsGroundClose;
        EnvironmentCheckersManager.WallChecker.TargetDetectionChangedEvent -= SetIsTouchingWall;
        EnvironmentCheckersManager.WallBackChecker.TargetDetectionChangedEvent -= SetIsTouchingWallBack;
        EnvironmentCheckersManager.LedgeChecker.TargetDetectionChangedEvent -= SetIsTouchingLedge;

        InputHandler.JumpInput.StateChangedEvent -= SetJumpInput;
        InputHandler.GrabInput.StateChangedEvent -= SetGrabInput;
    }

    public override void LogicUpdate() => base.LogicUpdate();

    private void SetIsGrounded(Boolean value) => IsGrounded = value;
    private void SetIsGroundClose(Boolean value) => IsGroundClose = value;
    private void SetIsTouchingWall(Boolean value) => IsTouchingWall = value;
    private void SetIsTouchingWallBack(Boolean value) => IsTouchingWallBack = value;
    private void SetIsTouchingLedge(Boolean value) => IsTouchingLedge = value;

    private void SetJumpInput(Boolean value) => JumpInput = value;
    private void SetGrabInput(Boolean value) => GrabInput = value;
}
