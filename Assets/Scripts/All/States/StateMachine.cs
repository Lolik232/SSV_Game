using System;

using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class StateMachine
{
    public PlayerState CurrentState { get; private set; }


    public event Action<PlayerState> StateEnterEvent;
    public event Action<PlayerState> StateExitEvent;

    public StateMachine(PlayerState initialState = null) => ChangeState(initialState);

    public void ChangeState(PlayerState newState)
    {
        if (newState == null) { return; }
        if (CurrentState != null)
        {
            CurrentState.Exit();
            OnStateExit(CurrentState);
        }
        CurrentState = newState;
        CurrentState.Enter();
        OnStateEnter(CurrentState);
    }

    protected virtual void OnStateEnter(PlayerState state)
    {
        StateEnterEvent?.Invoke(state);
    }

    protected virtual void OnStateExit(PlayerState state)
    {
        StateExitEvent?.Invoke(state);
    }
}
