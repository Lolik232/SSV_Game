using System;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(PlayerStatesManager statesManager,string animBoolName) : base(statesManager, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if  (IsActive)
        {
            if (InputX != 0)
            {
                StateMachine.ChangeState(StatesManager.MoveState);
            }
            else if (IsAnimationFinished)
            {
                StateMachine.ChangeState(StatesManager.IdleState);
            }
        }
    }
}
