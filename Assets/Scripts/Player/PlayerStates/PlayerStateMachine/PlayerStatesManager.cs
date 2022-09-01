using System;

using UnityEngine;

public class PlayerStatesManager
{

    public readonly Player Player;

    public PlayerData Data { get; private set; }

    public StateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }

    public PlayerLedgeClimbState LedgeClimbState { get; private set; }

    public PlayerStatesManager(Player player, PlayerData data)
    {
        Player = player;
        Data = data;

        IdleState = new PlayerIdleState(this, player, data, "idle");
        MoveState = new PlayerMoveState(this, player, data, "move");
        InAirState = new PlayerInAirState(this, player, data, "inAir");
        LandState = new PlayerLandState(this, player, data, "land");
        JumpState = new PlayerJumpState(this, player, data, "inAir");
        WallGrabState = new PlayerWallGrabState(this, player, data, "wallGrab");
        WallSlideState = new PlayerWallSlideState(this, player, data, "wallSlide");
        WallClimbState = new PlayerWallClimbState(this, player, data, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, player, data, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, player, data, "ledgeClimb");
        StateMachine = new StateMachine(IdleState);
    }
}
