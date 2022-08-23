using System;

public class PlayerState
{
    public TriggerAction IsActive { get; private set; }

    protected readonly PlayerStatesManager StatesManager;
    protected readonly StateMachine StateMachine;
    protected readonly PlayerMoveController MoveController;
    protected readonly PlayerInputHandler InputHandler;
    protected readonly EnvironmentCheckersManager EnvironmentCheckersManager;
    protected readonly PlayerData Data;

    protected Int32 InputX;
    protected Int32 InputY;

    protected Boolean IsAnimationFinished;

    public readonly String AnimBoolName;

    public PlayerState(PlayerStatesManager statesManager, String animBoolName)
    {
        StatesManager = statesManager;
        EnvironmentCheckersManager = StatesManager.EnvironmentCheckersManager;
        MoveController = StatesManager.MoveController;
        InputHandler = StatesManager.InputHandler;
        StateMachine = StatesManager.StateMachine;
        Data = StatesManager.Data;

        AnimBoolName = animBoolName;

        IsActive = new TriggerAction();
    }

    public virtual void Enter()
    {
        IsActive.Initiate();

        InputHandler.NormInputX.StateChangedEvent += SetInputX;
        InputHandler.NormInputY.StateChangedEvent += SetInputY;
    }

    public virtual void Exit()
    {
        IsActive.Terminate();

        InputHandler.NormInputX.StateChangedEvent -= SetInputX;
        InputHandler.NormInputY.StateChangedEvent -= SetInputY;
    }

    public virtual void LogicUpdate()
    {
        if (!IsActive) { return; }
    }

    private void SetInputX(Int32 value) => InputX = value;
    private void SetInputY(Int32 value) => InputY = value;
}
