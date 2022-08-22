using System;
using UnityEngine;

public class PlayerTouchingWallState : PlayerEnvironmentState
{
    public PlayerTouchingWallState(Player player, PlayerStatesManager statesDescriptor, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, statesDescriptor, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger(int id = 0)
    {
        base.AnimationTrigger(id);
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        Player.Endurance.DisableRegeneration();
    }

    public override void Exit()
    {
        base.Exit();

        Player.Endurance.EnableRegeneration();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (JumpInput)
        {

        }
        else if (IsGrounded && !GrabInput)
        {
            ChangeState(StatesDescriptor.IdleState);
        }
        else if (!IsTouchingWall || (InputX != Player.FacingDirection && !GrabInput))
        {
            ChangeState(StatesDescriptor.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
