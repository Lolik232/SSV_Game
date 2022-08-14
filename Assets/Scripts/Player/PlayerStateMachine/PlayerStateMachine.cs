using System;
using UnityEngine;

public class PlayerStateMachine
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
