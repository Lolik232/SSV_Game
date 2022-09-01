using System;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    public TimeDependentAction WallJumpTime { get; private set; }
    public PlayerWallJumpState(PlayerStatesManager statesManager, Player player, PlayerData data, string animBoolName) : base(statesManager, player, data, animBoolName)
    {
        WallJumpTime = new TimeDependentAction(data.wallJumpTime);
    }

    public override void Enter()
    {
        base.Enter();
        WallJumpTime.Initiate();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void InputUpdate()
    {
        base.InputUpdate();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!IsActive) { return; }

        if (!WallJumpTime)
        {
            IsAbilityDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected override void DoChecks()
    {
        base.DoChecks();
    }
}
