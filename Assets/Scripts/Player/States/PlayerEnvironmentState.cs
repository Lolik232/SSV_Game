using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnvironmentState : PlayerState
{
    protected bool isGrounded;
    protected bool isGroundFar;
    protected bool isTouchingWall;
    protected bool isTouchingWallBack;
    protected bool isTouchingLedge;

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    protected override void DoChecks()
    {
        base.DoChecks();
    }

    protected override void ReadInput()
    {
        base.ReadInput();
    }
}
