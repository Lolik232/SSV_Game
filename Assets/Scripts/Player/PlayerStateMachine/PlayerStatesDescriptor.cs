using System;
using UnityEngine;

public class PlayerStatesDescriptor
{
    private PlayerStateMachine _stateMachine;

    #region States Variables

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }

    private readonly PlayerState _defaultState;

    #endregion

    public PlayerStatesDescriptor(Player player, PlayerData data)
    {
        _stateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(player, this, _stateMachine, data, "idle");
        MoveState = new PlayerMoveState(player, this, _stateMachine, data, "move");
        InAirState = new PlayerInAirState(player, this, _stateMachine, data, "inAir");
        LandState = new PlayerLandState(player, this, _stateMachine, data, "land");

        _defaultState = IdleState;
    }

    public void LogicUpdate() => _stateMachine.CurrentState.LogicUpdate();

    public void PhysicsUpdate() => _stateMachine.CurrentState.PhysicsUpdate();

    public void AnimationTrigger(Int32 id = 0) => _stateMachine.CurrentState.AnimationTrigger(id);

    public void AnimationFinishTrigger() => _stateMachine.CurrentState.AnimationFinishTrigger();

    public void Initialize() => _stateMachine.Initialize(_defaultState);
}
