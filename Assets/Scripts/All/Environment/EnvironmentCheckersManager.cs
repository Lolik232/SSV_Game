using System;
using UnityEngine;

public class EnvironmentCheckersManager
{
    public GroundChecker GroundChecker { get; private set; }
    public BarrierChecker GroundCloseChecker { get; private set; }

    public BarrierChecker WallChecker { get; private set; }
    public BarrierChecker WallBackChecker { get; private set; }

    public BarrierChecker LedgeChecker { get; private set; }

    public MoveController MoveController { get; private set; }

    public EnvironmentCheckersManager(Transform groundChecker, Transform wallChecker, Transform ledgeChecker, MoveController moveController, UnitData data)
    {
        GroundChecker = new GroundChecker(groundChecker, data.groundCheckRadius, data.whatIsGround);
        GroundCloseChecker = new BarrierChecker(groundChecker, data.groundIsCloseCheckDistance, Vector2.down, data.whatIsGround);
        WallChecker = new BarrierChecker(wallChecker, data.wallCheckDistance, Vector2.right, data.whatIsGround);
        WallBackChecker = new BarrierChecker(wallChecker, data.wallCheckDistance, Vector2.left, data.whatIsGround);
        LedgeChecker = new BarrierChecker(ledgeChecker, data.wallCheckDistance, Vector2.right, data.whatIsGround);

        MoveController = moveController;
    }

    public void Initialize()
    {
        MoveController.FacingDirection.StateChangedEvent += WallChecker.OnFacingDirectionChanged;
        MoveController.FacingDirection.StateChangedEvent += WallBackChecker.OnFacingDirectionChanged;
        MoveController.FacingDirection.StateChangedEvent += LedgeChecker.OnFacingDirectionChanged;
    }

    public void PhysicsUpdate()
    {
        GroundChecker.CheckIfGrounded();
        GroundCloseChecker.CheckIfTouchingBarrier();
        WallChecker.CheckIfTouchingBarrier();
        WallBackChecker.CheckIfTouchingBarrier();
        LedgeChecker.CheckIfTouchingBarrier();
    }

    ~EnvironmentCheckersManager()
    {
        MoveController.FacingDirection.StateChangedEvent -= WallChecker.OnFacingDirectionChanged;
        MoveController.FacingDirection.StateChangedEvent -= WallBackChecker.OnFacingDirectionChanged;
        MoveController.FacingDirection.StateChangedEvent -= LedgeChecker.OnFacingDirectionChanged;
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
