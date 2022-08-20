using System;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStatesDescriptor statesDescriptor, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, statesDescriptor, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if  (IsActive)
        {
            if (InputX != 0)
            {
                ChangeState(StatesDescriptor.MoveState);
            }
            else if (IsAnimationFinished)
            {
                ChangeState(StatesDescriptor.IdleState);
            }
        }
    }
}
