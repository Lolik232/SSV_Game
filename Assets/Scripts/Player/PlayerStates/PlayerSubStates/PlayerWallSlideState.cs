using System;
using Unity.VisualScripting;

using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(PlayerStatesManager statesManager, Player player, PlayerData data, string animBoolName) : base(statesManager, player, data, animBoolName)
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

        if (Player.AbilitiesManager.WallClimbAbility.CanGrab && GrabInput && InputY >= 0)
{
            StatesManager.StateMachine.ChangeState(StatesManager.WallGrabState);
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
