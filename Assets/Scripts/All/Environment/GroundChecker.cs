using System;

using Unity.VisualScripting;

using UnityEngine;

public class GroundChecker : EnvironmentChecker
{
    private readonly Single m_Radius;
    public GroundChecker(Transform checker, Single radius, LayerMask whatIsTarget) : base(checker, whatIsTarget)
    {
        m_Radius = radius;
    }

    public void CheckIfGrounded()
    {
        IsDetected = Physics2D.OverlapCircle(Position, m_Radius, WhatIsTarget);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Position, m_Radius);
    }
}
