using System;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected Boolean IsGrounded;

    protected Boolean IsAbilityDone;
    public PlayerAbilityState(PlayerStatesManager statesManager, string animBoolName) : base(statesManager, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        EnvironmentCheckersManager.GroundChecker.TargetDetectionChangedEvent += SetIsGrounded;

        IsAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();

        EnvironmentCheckersManager.GroundChecker.TargetDetectionChangedEvent -= SetIsGrounded;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAbilityDone)
        {
            if (IsGrounded && MoveController.CurrentVelocityY < Data.groundSlopeTolerance)
            {
                StateMachine.ChangeState(StatesManager.IdleState);
            }
            else
            {
                StateMachine.ChangeState(StatesManager.InAirState);
            }
        } 
    }

    private void SetIsGrounded(Boolean value) => IsGrounded = value;
}
