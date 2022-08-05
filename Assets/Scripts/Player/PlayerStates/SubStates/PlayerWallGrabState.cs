using System;
using UnityEngine;
public class PlayerWallGrabState : PlayerTouchingWallState
{
    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
}
