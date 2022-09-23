using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : State
{
    [SerializeField] protected readonly Player Player;

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
        ReadInput();
    }

    protected override void DoChecks()
    {
        base.DoChecks();
    }

    protected virtual void ReadInput() { }
}
