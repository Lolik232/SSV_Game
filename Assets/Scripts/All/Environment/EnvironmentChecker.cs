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

    protected Vector2 Position => Checker.position;

    protected LayerMask WhatIsTarget;

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
