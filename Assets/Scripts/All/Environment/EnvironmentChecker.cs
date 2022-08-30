using System;

using UnityEngine;

public abstract class EnvironmentChecker
{
    private bool m_IsDetected;
    public bool IsDetected
    {
        get
        {
            return m_IsDetected;
        }
        protected set
        {
            if (m_IsDetected != value)
            {
                m_IsDetected = value;
                if (value)
                {
                    OnTargetDetected();
                }
                else
                {
                    OnTargetLost();
                }
            }
        }
            
    }

    protected Transform Checker;

    public Vector2 Position => Checker.position;

    public LayerMask WhatIsTarget { get; private set; }

    public event Action TargetDetectedEvent;
    public event Action TargetLostEvent;

    public EnvironmentChecker(Transform checker, LayerMask whatIsTarget)
    {
        Checker = checker;
        WhatIsTarget = whatIsTarget;
    }

    protected virtual void OnTargetDetected()
    {
        TargetDetectedEvent?.Invoke();
    }
    protected virtual void OnTargetLost()
    {
        TargetLostEvent?.Invoke();
    }
}
