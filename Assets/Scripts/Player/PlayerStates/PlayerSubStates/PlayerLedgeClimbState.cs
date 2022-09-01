using System;

using UnityEngine;
using UnityEngine.Windows;

using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerLedgeClimbState : PlayerEnvironmentState
{
    public TriggerAction IsHanging { get; private set; }
    public TriggerAction IsClimbing { get; private set; }

    public PlayerLedgeClimbState(PlayerStatesManager statesManager, Player player, PlayerData data, string animBoolName) : base(statesManager, player, data, animBoolName)
    {
        IsHanging = new TriggerAction();
        IsClimbing = new TriggerAction();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        IsClimbing.Terminate();
        IsHanging.Terminate();
    }

    public override void InputUpdate()
    {
        base.InputUpdate();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!IsActive) { return; }

        if (Player.AbilitiesManager.WallClimbAbility.CanLedgeClimb && InputX == Player.MoveController.FacingDirection && IsHanging && !IsClimbing)
        {
            IsClimbing.Initiate();
        }
        else if (Player.CharacteristicsManager.Endurance.IsEmpty() || (InputY == -1 && IsHanging && !IsClimbing))
        {
            StatesManager.StateMachine.ChangeState(StatesManager.InAirState);
        }
        else if (Player.AbilitiesManager.JumpAbility.CanWallJump && JumpInput && !IsClimbing)
        {
            StatesManager.StateMachine.ChangeState(StatesManager.WallJumpState);
        }
    }

    public void OnLedgeHanging()
    {
        IsHanging.Initiate();
    }

    public void OnLedgeClimbEnd()
    {
        StatesManager.StateMachine.ChangeState(StatesManager.IdleState);
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
