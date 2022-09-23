using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerIdleState", menuName = "Player/States/Idle")]
public class PlayerIdleState : PlayerGroundedState
{
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
