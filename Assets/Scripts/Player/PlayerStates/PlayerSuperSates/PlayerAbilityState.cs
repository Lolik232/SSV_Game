using System;

using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool IsGrounded;

    protected bool IsAbilityDone;

    public PlayerAbilityState(PlayerStatesManager statesManager, Player player, PlayerData data, string animBoolName) : base(statesManager, player, data, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        IsAbilityDone = false;
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
        if (IsAbilityDone)
        {
            if (IsGrounded)
            {
                StatesManager.StateMachine.ChangeState(StatesManager.IdleState);
            }
            else
            {
                StatesManager.StateMachine.ChangeState(StatesManager.InAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected override void DoChecks()
    {
        base.DoChecks();
        IsGrounded = Player.EnvironmentCheckersManager.GroundChecker.IsDetected;
    }
}
