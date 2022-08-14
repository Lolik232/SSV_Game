using System;
using UnityEngine;

public class PlayerGroundedState : PlayerEnvironmentState
{
    public PlayerGroundedState(Player player, PlayerStatesDescriptor statesDescriptor, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, statesDescriptor, stateMachine, playerData, animBoolName)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_isGrounded)
        {
            ChangeState(_statesDescriptor.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
