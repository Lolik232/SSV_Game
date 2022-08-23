using System;

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

    public virtual void Initiate()
    {
        m_IsActive = true;
    }

    public virtual void Terminate()
    {
        m_IsActive = false;
    }

    public static Boolean operator true(TriggerState state) => state.IsActive;

    public static Boolean operator false(TriggerState state) => state.IsActive;

    public static Boolean operator !(TriggerState state) => !state.IsActive;

    public static Boolean operator &(TriggerState lhs, TriggerState rhs) => lhs.IsActive && rhs.IsActive;

    public static Boolean operator |(TriggerState lhs, TriggerState rhs) => lhs.IsActive || rhs.IsActive;

    public static Boolean operator ==(TriggerState lhs, TriggerState rhs) => lhs.IsActive == rhs.IsActive;

    public static Boolean operator !=(TriggerState lhs, TriggerState rhs) => lhs.IsActive != rhs.IsActive;

    public override Boolean Equals(object obj)
    {
        return obj is TriggerState state &&
               IsActive == state.IsActive;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(IsActive);
    }

    private void SendStateChanged(Boolean isActive)
    {
        StateChangedEvent?.Invoke(isActive);
    }
}

