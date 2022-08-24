using System;

using UnityEngine;

[RequireComponent(typeof(EnvironmentCheckersManager), typeof(PlayerMoveController), typeof(PlayerInputHandler))]
public class PlayerStatesManager : MonoBehaviour
{
    public EnvironmentCheckersManager EnvironmentCheckersManager { get; private set; }
    public PlayerMoveController MoveController { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }

    [SerializeField] private PlayerData m_Data;
    public PlayerData Data { get => m_Data; private set => m_Data = value; }

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
        EnvironmentCheckersManager = GetComponent<EnvironmentCheckersManager>();
        MoveController = GetComponent<PlayerMoveController>();
        InputHandler = GetComponent<PlayerInputHandler>();

        StateMachine = new StateMachine();
    }

    private void Start()
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

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }
}
