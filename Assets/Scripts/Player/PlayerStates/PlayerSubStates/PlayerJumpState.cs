using System;
using UnityEngine;

public class PlayerJumpState: PlayerAbilityState
{ 
    public PlayerJumpState(PlayerStatesManager statesManager, Player player, PlayerData data, string animBoolName) : base(statesManager, player, data, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StatesManager.InAirState.Jumping.Initiate();
        IsGroundedBlock = true;
        IsAbilityDone = true;
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
