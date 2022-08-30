using System;

using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public event Action MoveEvent;

    public PlayerMoveState(PlayerStatesManager statesManager, Player player, PlayerData data, string animBoolName) : base(statesManager, player, data, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
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

        if (InputX == 0)
        {
            StatesManager.StateMachine.ChangeState(StatesManager.IdleState);
        }
        else
        {
            OnMove();
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

    protected override void OnExit()
    {
        base.OnExit();
    }

    protected virtual void OnMove()
    {
        MoveEvent?.Invoke();
    }
}
