using System;
using All.BaseClasses;
using All.Interfaces.Channel;
using UnityEngine.Events;

namespace All.Events
{
    public class TypedEventChannelSO<T> : BaseDescriptionSO,IReadChannel<T>, IWriteChannel<T>
    {
        public event UnityAction<T> OnEventRaised = default;

        public virtual void RaiseEvent(T value)
        {
            OnEventRaised?.Invoke(value);
        }
    }

    public class TypedEventChannelSO<T1, T2> : BaseDescriptionSO,IReadChannel<T1,T2>, IWriteChannel<T1,T2>
    {
        public event UnityAction<T1, T2> OnEventRaised = default;

        public virtual void RaiseEvent(T1 value1, T2 value2)
        {
            OnEventRaised?.Invoke(value1, value2);
        }
    }

    public class TypedEventChannelSO<T1, T2, T3> : BaseDescriptionSO, IReadChannel<T1,T2,T3>, IWriteChannel<T1,T2,T3>
    {
        public event UnityAction<T1, T2, T3> OnEventRaised = default;

        public virtual void RaiseEvent(T1 value1, T2 value2, T3 value3)
        {
            OnEventRaised?.Invoke(value1, value2, value3);
        }
    }


    public class EventChannelSO : BaseDescriptionSO,IReadChannel, IWriteChannel
    {
        public event UnityAction OnEventRaised;

        public void RaiseEvent()
        {
            OnEventRaised?.Invoke();
        }
    }
}