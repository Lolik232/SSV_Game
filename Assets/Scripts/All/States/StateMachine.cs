using System;

using UnityEngine;

public class StateMachine
{
    private PlayerState m_CurrentState;
    public PlayerState CurrentState
    {
        get
        {
            return m_CurrentState;
        }
        private set
        {
            if (m_CurrentState != value)
            {
                SendStateChanged(m_CurrentState = value);
            }
        }
    }

    public event Action<PlayerState> StateChangedEvent;

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

    private void SendStateChanged(PlayerState newState)
    {
        StateChangedEvent?.Invoke(newState);
    }
}
