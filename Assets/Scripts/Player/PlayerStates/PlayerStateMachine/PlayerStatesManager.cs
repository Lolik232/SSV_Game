using System;

using UnityEngine;

[RequireComponent(typeof(EnvironmentCheckersManager), typeof(PlayerMoveController))]
public class PlayerStatesManager : MonoBehaviour
{
    public EnvironmentCheckersManager EnvironmentCheckersManager { get; private set; }
    public PlayerMoveController MoveController { get; private set; }

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
        StateMachine = new StateMachine();

        IdleState = new PlayerIdleState(this, "idle");
        MoveState = new PlayerMoveState(player, this, m_StateMachine, data, "move");
        InAirState = new PlayerInAirState(player, this, m_StateMachine, data, "inAir");
        LandState = new PlayerLandState(player, this, m_StateMachine, data, "land");
        JumpState = new PlayerJumpState(player, this, m_StateMachine, data, "inAir");
        WallGrabState = new PlayerWallGrabState(player, this, m_StateMachine, data, "wallGrab");
        WallSlideState = new PlayerWallSlideState(player, this, m_StateMachine, data, "wallSlide");
        WallClimbState = new PlayerWallClimbState(player, this, m_StateMachine, data, "wallClimb");
    }

    private void Start()
    {

    }
}
