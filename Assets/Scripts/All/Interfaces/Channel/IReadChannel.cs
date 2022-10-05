using System;
using UnityEngine.Events;

namespace All.Interfaces.Channel
{
    public interface IReadChannel
    {
        public event UnityAction OnEventRaised;
    }

    public interface IReadChannel<T>
    {
        public event UnityAction<T> OnEventRaised;
    }

    public interface IReadChannel<T1, T2>
    {
        public event UnityAction<T1, T2> OnEventRaised;
    }

    public interface IReadChannel<T1, T2, T3>
    {
        public event UnityAction<T1, T2, T3> OnEventRaised;
    }
}