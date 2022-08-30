using System;

using UnityEngine;

public abstract class PlayerState : IPlayerState
{
    protected readonly PlayerStatesManager StatesManager;
    protected readonly Player Player;
    protected readonly PlayerData Data;

    protected int InputX;
    protected int InputY;

    protected Vector2 Velocity { get; private set; }

    protected bool IsActive { get; private set; }

    public readonly string AnimBoolName;

    protected PlayerState(PlayerStatesManager statesManager, Player player, PlayerData data, string animBoolName)
    {
        StatesManager = statesManager;
        Player = player;
        Data = data;
        AnimBoolName = animBoolName;
    }

    public event Action EnterEvent;
    public event Action ExitEvent;


    public virtual void Enter()
    {
        IsActive = true;
        DoChecks();
        OnEnter();
    }

    public virtual void Exit()
    {
        IsActive = false;
        OnExit();
    }

    public virtual void InputUpdate()
    {
        InputX = Player.InputHandler.NormInputX;
        InputY = Player.InputHandler.NormInputY;
    }

    public virtual void LogicUpdate()
    {
        if (!IsActive) { return; }

        InputUpdate();

        Velocity = Player.MoveController.CurrentVelocity;
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    protected virtual void DoChecks()
    {

    }

    protected virtual void OnEnter()
    {
        EnterEvent?.Invoke();
    }

    protected virtual void OnExit()
    {
        ExitEvent?.Invoke();
    }
}
