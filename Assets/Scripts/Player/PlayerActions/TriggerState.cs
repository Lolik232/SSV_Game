using System;

using UnityEngine;

public class TriggerState
{
    private Boolean m_IsActive;

    public Boolean IsActive
    {
        get { return m_IsActive; }
        protected set
        {
            if (m_IsActive != value)
            {
                SendStateChanged(m_IsActive = value);
            }
        }
    }

    public event Action<Boolean> StateChangedEvent;

    public virtual void Start()
    {
        m_IsActive = true;
    }

    public virtual void End()
    {
        m_IsActive = false;
    }

    private void SendStateChanged(Boolean isActive)
    {
        StateChangedEvent?.Invoke(isActive);
    }
}

