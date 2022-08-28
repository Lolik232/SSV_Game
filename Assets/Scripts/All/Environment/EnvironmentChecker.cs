using System;

using UnityEngine;

public abstract class EnvironmentChecker
{
    private Boolean m_IsDetected;
    public Boolean IsDetected
    {
        get
        {
            return m_IsDetected;
        }
        protected set
        {
            if (m_IsDetected != value)
            {
                SendTargetDetectionChanged(m_IsDetected = value);
            }
        }
            
    }

    protected Transform Checker;

    public Vector2 Position => Checker.position;

    public LayerMask WhatIsTarget { get; private set; }

    public event Action<Boolean> TargetDetectionChangedEvent;

    public EnvironmentChecker(Transform checker, LayerMask whatIsTarget)
    {
        Checker = checker;
        WhatIsTarget = whatIsTarget;
    }

    private void SendTargetDetectionChanged(Boolean isDetected)
    {
        TargetDetectionChangedEvent?.Invoke(isDetected);
    }
}
