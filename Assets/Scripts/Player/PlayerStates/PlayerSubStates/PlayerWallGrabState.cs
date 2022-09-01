using System;

using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    public PlayerWallGrabState(PlayerStatesManager statesManager, Player player, PlayerData data, string animBoolName) : base(statesManager, player, data, animBoolName)
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

        if (Player.AbilitiesManager.WallClimbAbility.CanWallClimb && InputY > 0)
        {
            StatesManager.StateMachine.ChangeState(StatesManager.WallClimbState);
        }
        else if (Player.CharacteristicsManager.Endurance.IsEmpty() || InputY < 0 || !GrabInput)
        {
            StatesManager.StateMachine.ChangeState(StatesManager.WallSlideState);
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
