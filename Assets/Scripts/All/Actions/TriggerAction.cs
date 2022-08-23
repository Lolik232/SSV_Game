using System;

public class TriggerAction
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

    public TriggerAction()
    {
        m_IsActive = false;
    }

    public virtual void Initiate()
    {
        m_IsActive = true;
    }

    public virtual void Terminate()
    {
        m_IsActive = false;
    }

    public static implicit operator Boolean(TriggerAction a) => a.IsActive;

    public static Boolean operator ==(TriggerAction lhs, TriggerAction rhs) => lhs.IsActive == rhs.IsActive;
    public static Boolean operator !=(TriggerAction lhs, TriggerAction rhs) => lhs.IsActive != rhs.IsActive;

    public override Boolean Equals(object obj) => obj is TriggerAction action && this == action;

    public override Int32 GetHashCode() => HashCode.Combine(IsActive);

    private void SendStateChanged(Boolean isActive)
    {
        StateChangedEvent?.Invoke(isActive);
    }
}

