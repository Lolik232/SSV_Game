using System;

using UnityEngine;

public class EnvironmentCheckersManager
{
    public readonly Unit Unit;
    public GroundChecker GroundChecker { get; private set; }
    public BarrierChecker GroundCloseChecker { get; private set; }

    public BarrierChecker WallChecker { get; private set; }
    public BarrierChecker WallBackChecker { get; private set; }

    public BarrierChecker LedgeChecker { get; private set; }

    public MoveController MoveController { get; private set; }

    public TriggerAction IsLedgeDetected { get; private set; }

    public EnvironmentCheckersManager(Transform groundChecker, Transform wallChecker, Transform ledgeChecker, Unit unit, UnitData data)
    {
        GroundChecker = new GroundChecker(groundChecker, data.groundCheckRadius, data.whatIsGround);
        GroundCloseChecker = new BarrierChecker(groundChecker, data.groundIsCloseCheckDistance, Vector2.down, data.whatIsGround);
        WallChecker = new BarrierChecker(wallChecker, data.wallCheckDistance, Vector2.right, data.whatIsGround);
        WallBackChecker = new BarrierChecker(wallChecker, data.wallCheckDistance, Vector2.left, data.whatIsGround);
        LedgeChecker = new BarrierChecker(ledgeChecker, data.wallCheckDistance, Vector2.right, data.whatIsGround);
        Unit = unit;

        IsLedgeDetected = new TriggerAction();
    }

    public void Initialize()
    {
        Unit.MoveController.FacingDirection.StateChangedEvent += WallChecker.OnFacingDirectionChanged;
        Unit.MoveController.FacingDirection.StateChangedEvent += WallBackChecker.OnFacingDirectionChanged;
        Unit.MoveController.FacingDirection.StateChangedEvent += LedgeChecker.OnFacingDirectionChanged;
    }

    public void PhysicsUpdate()
    {
        GroundChecker.CheckIfGrounded();
        GroundCloseChecker.CheckIfTouchingBarrier();
        WallChecker.CheckIfTouchingBarrier();
        WallBackChecker.CheckIfTouchingBarrier();
        LedgeChecker.CheckIfTouchingBarrier();

        IsLedgeDetected.IsActive = !LedgeChecker.IsDetected && WallChecker.IsDetected;
    }

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(WallChecker.Position, WallChecker.Direction, WallChecker.Distance, WallChecker.WhatIsTarget);
        float xDist = xHit.distance;
        Vector2 workspace = (xDist + 0.015f) * WallChecker.Direction;
        RaycastHit2D yHit = Physics2D.Raycast(LedgeChecker.Position + workspace, Vector2.down, LedgeChecker.Position.y - WallChecker.Position.y + 0.015f, WallChecker.WhatIsTarget);
        float yDist = yHit.distance;
        workspace.Set(WallChecker.Position.x + (xDist * WallChecker.Direction.x), LedgeChecker.Position.y - yDist);

        return workspace;
    }
    public int DetermineWallJumpDirection()
    {
        return WallChecker.IsDetected ? -Unit.MoveController.FacingDirection : Unit.MoveController.FacingDirection;
    }

    public void OnDrawGizmos()
    {
        GroundChecker?.OnDrawGizmos();
        GroundCloseChecker?.OnDrawGizmos();
        WallChecker?.OnDrawGizmos();
        WallBackChecker?.OnDrawGizmos();
        LedgeChecker?.OnDrawGizmos();
    }
}