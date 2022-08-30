using System;

public class TriggerAction
{
    private bool m_IsActive;
    public bool IsActive
    {
        get { return m_IsActive; }
        set
        {
            if (m_IsActive != value)
            {
                m_IsActive = value;
                if (value)
                {
                    OnActive();
                }
                else
                {
                    OnInactive();
                }
            }
        }
    }

    public event Action ActiveEvent;
    public event Action InactiveEvent;

    public TriggerAction()
    {
        m_IsActive = false;
    }

    public virtual void Initiate()
    {
        IsActive = true;
    }

    public virtual void Terminate()
    {
        IsActive = false;
    }

    public static implicit operator bool(TriggerAction a) => a.IsActive;

    public static bool operator ==(TriggerAction lhs, TriggerAction rhs) => lhs.IsActive == rhs.IsActive;
    public static bool operator !=(TriggerAction lhs, TriggerAction rhs) => lhs.IsActive != rhs.IsActive;

    public override bool Equals(object obj) => obj is TriggerAction action && this == action;

    public override int GetHashCode() => HashCode.Combine(IsActive);

    private void OnActive()
    {
        ActiveEvent?.Invoke();
    }

    private void OnInactive()
    {
        InactiveEvent?.Invoke();
    }
}

