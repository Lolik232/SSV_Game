using System;

using UnityEngine;

public class PlayerState
{
    public TriggerState IsActive { get; private set; }

    protected readonly PlayerStatesManager StatesDescriptor;
    protected readonly StateMachine StateMachine;
    protected readonly PlayerMoveController MoveController;
    protected readonly PlayerInputHandler InputHandler;
    protected readonly EnvironmentCheckersManager EnvironmentCheckersManager;
    protected readonly PlayerData Data;

    protected Int32 InputX;
    protected Int32 InputY;

    public readonly String AnimBoolName;

    public PlayerState(PlayerStatesManager statesDescriptor, String animBoolName)
    {
        StatesDescriptor = statesDescriptor;
        EnvironmentCheckersManager = StatesDescriptor.EnvironmentCheckersManager;
        MoveController = StatesDescriptor.MoveController;
        InputHandler = MoveController.PlayerInputHandler;
        StateMachine = StatesDescriptor.StateMachine;
        Data = StatesDescriptor.Data;

        AnimBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        IsActive.Initiate();
    }

    public virtual void Exit() => IsActive.Terminate();

    public virtual void LogicUpdate()
    {
        if (!IsActive)
        {
            return;
        }

        InputX = InputHandler.NormInputX;
        InputY = InputHandler.NormInputY;
    }
}
