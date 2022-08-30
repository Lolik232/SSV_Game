using System;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    private bool IsLandFinished;

    public PlayerLandState(PlayerStatesManager statesManager, Player player, PlayerData data, string animBoolName) : base(statesManager, player, data, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        IsLandFinished = false;
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

        if (InputX != 0)
        {
            StatesManager.StateMachine.ChangeState(StatesManager.MoveState);
        }
        else if (IsLandFinished)
        {
            StatesManager.StateMachine.ChangeState(StatesManager.IdleState);
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

    public void OnLandFinished()
    {
        IsLandFinished = true;
    }
}
