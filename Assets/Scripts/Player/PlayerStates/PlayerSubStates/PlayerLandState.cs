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

        if  (_isActive)
        {
            if (_xInput != 0)
            {
                ChangeState(_statesDescriptor.MoveState);
            }
            else if (_isAnimationFinished)
            {
                ChangeState(_statesDescriptor.IdleState);
            }
        }
    }
}
