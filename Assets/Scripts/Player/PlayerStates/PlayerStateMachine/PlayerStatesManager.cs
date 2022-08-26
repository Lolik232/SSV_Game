using System;

using UnityEngine;

public class PlayerStatesManager
{
    public EnvironmentCheckersManager EnvironmentCheckersManager { get; private set; }
    public PlayerMoveController MoveController { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
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

    public PlayerStatesManager(EnvironmentCheckersManager environmentCheckersManager, PlayerMoveController moveController, PlayerInputHandler inputHandler, PlayerData data)
    {
        EnvironmentCheckersManager = environmentCheckersManager;
        MoveController = moveController;
        InputHandler = inputHandler;
        Data = data;

        StateMachine = new StateMachine();
    }

    public void Initialize()
    {
        IdleState = new PlayerIdleState(this, "idle");
        MoveState = new PlayerMoveState(this, "move");
        InAirState = new PlayerInAirState(this, "inAir");
        LandState = new PlayerLandState(this, "land");
        JumpState = new PlayerJumpState(this, "inAir");
        WallGrabState = new PlayerWallGrabState(this, "wallGrab");
        WallSlideState = new PlayerWallSlideState(this, "wallSlide");
        WallClimbState = new PlayerWallClimbState(this, "wallClimb");

        StateMachine.Initialize(IdleState);
    }

    public void LogicUpdate()
    {
        StateMachine.CurrentState.LogicUpdate();
    }
}
