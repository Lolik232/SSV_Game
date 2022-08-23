using System;
using System.Collections.Generic;

using UnityEngine;

public class ValueChangingAction<T> where T : IEquatable<T>, IComparable<T>
{
    private T m_Value;
    public T Value
    {
        get { return m_Value; }
        set
        {
            if (!m_Value.Equals(value))
            {
                SendStateChanged(m_Value = value);
            }
        }
    }

    public ValueChangingAction()
    {
        m_Value = default;
    }

    public event Action<T> StateChangedEvent;

    public static implicit operator T(ValueChangingAction<T> vca) => vca.Value;

    public static Boolean operator ==(ValueChangingAction<T> lhs, ValueChangingAction<T> rhs) => lhs.Equals(rhs);
    public static Boolean operator !=(ValueChangingAction<T> lhs, ValueChangingAction<T> rhs) => !lhs.Equals(rhs);
    public static Boolean operator >(ValueChangingAction<T> lhs, ValueChangingAction<T> rhs) => lhs.Value.CompareTo(rhs.Value) > 0;
    public static Boolean operator <(ValueChangingAction<T> lhs, ValueChangingAction<T> rhs) => lhs.Value.CompareTo(rhs.Value) < 0;
    public static Boolean operator >=(ValueChangingAction<T> lhs, ValueChangingAction<T> rhs) => lhs.Value.CompareTo(rhs.Value) >= 0;
    public static Boolean operator <=(ValueChangingAction<T> lhs, ValueChangingAction<T> rhs) => lhs.Value.CompareTo(rhs.Value) <= 0;

    public override bool Equals(object obj)
    {
        return obj is ValueChangingAction<T> action &&
               EqualityComparer<T>.Default.Equals(Value, action.Value);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
    }

    private void SendStateChanged(T value)
    {
        StateChangedEvent?.Invoke(value);
    }
}
