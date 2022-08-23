using System;

using UnityEngine;

[RequireComponent(typeof(EnvironmentCheckersManager))]
public class PlayerStatesManager : MonoBehaviour
{
    public EnvironmentCheckersManager EnvironmentCheckersManager { get; private set; }
    public PlayerMoveController MoveController { get; private set; }
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

    private void Awake()
    {
        StateMachine = new StateMachine();
    }

    private void Start()
    {
        EnvironmentCheckersManager = GetComponent<EnvironmentCheckersManager>();
        MoveController = (PlayerMoveController)EnvironmentCheckersManager.MoveController;
        Data = (PlayerData)EnvironmentCheckersManager.Data;

        IdleState = new PlayerIdleState(this, "idle");
        MoveState = new PlayerMoveState(this, "move");
        InAirState = new PlayerInAirState(this, "inAir");
        LandState = new PlayerLandState(this, "land");
        JumpState = new PlayerJumpState(this, "inAir");
        WallGrabState = new PlayerWallGrabState(this, "wallGrab");
        WallSlideState = new PlayerWallSlideState(this, "wallSlide");
        WallClimbState = new PlayerWallClimbState(this, "wallClimb");
    }
}
