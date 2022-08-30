using System;

using UnityEngine;

public class GroundChecker : EnvironmentChecker
{
    public readonly float Radius;
    public GroundChecker(Transform checker, float radius, LayerMask whatIsTarget) : base(checker, whatIsTarget)
    {
        Radius = radius;
    }

    public void CheckIfGrounded()
    {
        IsDetected = Physics2D.OverlapCircle(Position, Radius, WhatIsTarget);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Position, Radius);
    }
}
