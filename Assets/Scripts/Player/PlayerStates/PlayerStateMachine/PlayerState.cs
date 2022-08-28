using System;

using UnityEngine;

public class PlayerState
{
    protected Boolean IsActive;

    protected readonly PlayerStatesManager StatesManager;
    protected readonly StateMachine StateMachine;
    protected readonly PlayerMoveController MoveController;
    protected readonly PlayerInputHandler InputHandler;
    protected readonly EnvironmentCheckersManager EnvironmentCheckersManager;
    protected readonly PlayerAbilitiesManager AbilitiesManager;
    protected readonly PlayerData Data;

    protected Int32 InputX { get; private set; }
    protected Int32 InputY { get; private set; }

    protected Single VelocityX { get; private set; }
    protected Single VelocityY { get; private set; }

    protected Boolean IsAnimationFinished;

    public readonly String AnimBoolName;

    public PlayerState(PlayerStatesManager statesManager, String animBoolName)
    {
        StatesManager = statesManager;
        EnvironmentCheckersManager = StatesManager.EnvironmentCheckersManager;
        MoveController = StatesManager.MoveController;
        InputHandler = StatesManager.InputHandler;
        StateMachine = StatesManager.StateMachine;
        AbilitiesManager = StatesManager.AbilitiesManager;
        Data = StatesManager.Data;

        AnimBoolName = animBoolName;

        IsActive = new TriggerAction();
    }

    public event Action EnterEvent;
    public event Action ExitEvent;


    public virtual void Initialize()
    {

    }

    public virtual void Enter()
    {
        IsActive = true;

        InputX = InputHandler.NormInputX;
        InputY = InputHandler.NormInputY;
        VelocityX = MoveController.CurrentVelocityX;
        VelocityY = MoveController.CurrentVelocityY;

        InputHandler.NormInputX.StateChangedEvent += SetInputX;
        InputHandler.NormInputY.StateChangedEvent += SetInputY;
        MoveController.CurrentVelocityX.StateChangedEvent += SetVelocityX;
        MoveController.CurrentVelocityY.StateChangedEvent += SetVelocityY;

        Debug.Log(AnimBoolName);

        SendEnter();
    }

    public virtual void Exit()
    {

        IsActive = false;

        InputHandler.NormInputX.StateChangedEvent -= SetInputX;
        InputHandler.NormInputY.StateChangedEvent -= SetInputY;
        MoveController.CurrentVelocityX.StateChangedEvent -= SetVelocityX;
        MoveController.CurrentVelocityY.StateChangedEvent -= SetVelocityY;

        SendExit();
    }

    public virtual void LogicUpdate()
    {
        if (!IsActive) { return; }
    }

    private void SetInputX(Int32 value) => InputX = value;
    private void SetInputY(Int32 value) => InputY = value;
    private void SetVelocityX(Single value) => VelocityX = value;
    private void SetVelocityY(Single value) => VelocityY = value;

    protected virtual void SendEnter()
    {
        EnterEvent?.Invoke();
    }

    protected virtual void SendExit()
    {
        ExitEvent?.Invoke();
    }
}
