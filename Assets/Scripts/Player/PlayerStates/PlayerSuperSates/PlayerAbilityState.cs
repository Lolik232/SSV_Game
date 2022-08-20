using System;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    private Boolean _isGrounded;
    protected Boolean IsAbilityDone;
    public PlayerAbilityState(Player player, PlayerStatesDescriptor statesDescriptor, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, statesDescriptor, stateMachine, playerData, animBoolName)
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

        _isGrounded = Player.CheckIfGrounded();
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAbilityDone)
        {
            if (_isGrounded && Player.CurrentVelocity.y < Data.groundSlopeTolerance)
            {
                ChangeState(StatesDescriptor.IdleState);
            }
            else
            {
                ChangeState(StatesDescriptor.InAirState);
            }
        } 
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
