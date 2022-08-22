using System;
using UnityEngine;

public class StateMachine
{
    public PlayerState CurrentState { get; private set; }

    public void Initialize(PlayerState initialState)
    {
        CurrentState = initialState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
