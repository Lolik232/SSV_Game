using System;
using UnityEngine;

public class PlayerStatesDescriptor
{
    private PlayerStateMachine m_StateMachine;

    #region States Variables

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }

    private readonly PlayerState m_DefaultState;

    #endregion

    public PlayerStatesDescriptor(Player player, PlayerData data)
    {
        m_StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(player, this, m_StateMachine, data, "idle");
        MoveState = new PlayerMoveState(player, this, m_StateMachine, data, "move");
        InAirState = new PlayerInAirState(player, this, m_StateMachine, data, "inAir");
        LandState = new PlayerLandState(player, this, m_StateMachine, data, "land");
        JumpState = new PlayerJumpState(player, this, m_StateMachine, data, "inAir");
        WallGrabState = new PlayerWallGrabState(player, this, m_StateMachine, data, "wallGrab");
        WallSlideState = new PlayerWallSlideState(player, this, m_StateMachine, data, "wallSlide");
        WallClimbState = new PlayerWallClimbState(player, this, m_StateMachine, data, "wallClimb");

        m_DefaultState = IdleState;
    }

    public void LogicUpdate() => m_StateMachine.CurrentState.LogicUpdate();

    public void PhysicsUpdate() => m_StateMachine.CurrentState.PhysicsUpdate();

    public void AnimationTrigger(Int32 id = 0) => m_StateMachine.CurrentState.AnimationTrigger(id);

    public void AnimationFinishTrigger() => m_StateMachine.CurrentState.AnimationFinishTrigger();

    public void InitializeStateMachine() => m_StateMachine.Initialize(m_DefaultState);
}
