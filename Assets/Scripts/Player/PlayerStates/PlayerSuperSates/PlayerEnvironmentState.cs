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

    public PlayerEnvironmentState(PlayerStatesManager statesDescriptor, string animBoolName) : base(statesDescriptor, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        EnvironmentCheckersManager.GroundChecker.TargetDetectionChangedEvent += SetIsGrounded;
        EnvironmentCheckersManager.GroundCloseChecker.TargetDetectionChangedEvent += SetIsGroundClose;
        EnvironmentCheckersManager.WallChecker.TargetDetectionChangedEvent += SetIsTouchingWall;
        EnvironmentCheckersManager.WallBackChecker.TargetDetectionChangedEvent += SetIsTouchingWallBack;
        EnvironmentCheckersManager.LedgeChecker.TargetDetectionChangedEvent += SetIsTouchingLedge;
    }

    public override void Exit()
    {
        base.Exit();

        EnvironmentCheckersManager.GroundChecker.TargetDetectionChangedEvent -= SetIsGrounded;
        EnvironmentCheckersManager.GroundCloseChecker.TargetDetectionChangedEvent -= SetIsGroundClose;
        EnvironmentCheckersManager.WallChecker.TargetDetectionChangedEvent -= SetIsTouchingWall;
        EnvironmentCheckersManager.WallBackChecker.TargetDetectionChangedEvent -= SetIsTouchingWallBack;
        EnvironmentCheckersManager.LedgeChecker.TargetDetectionChangedEvent -= SetIsTouchingLedge;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        JumpInput = InputHandler.JumpInput.IsActive;
        GrabInput = InputHandler.GrabInput.IsActive;
    }

    private void SetIsGrounded(Boolean value) => IsGrounded = value;
    private void SetIsGroundClose(Boolean value) => IsGroundClose = value;
    private void SetIsTouchingWall(Boolean value) => IsTouchingWall = value;
    private void SetIsTouchingWallBack(Boolean value) => IsTouchingWallBack = value;
    private void SetIsTouchingLedge(Boolean value) => IsTouchingLedge = value;

}
