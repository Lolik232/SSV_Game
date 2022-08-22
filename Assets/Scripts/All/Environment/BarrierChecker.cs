using System;
using UnityEngine;

public class BarrierChecker : EnvironmentChecker
{
    private readonly Single m_Distance;

    private Vector2 m_Direction;

    public BarrierChecker(Transform checker, Single distance, Vector2 direction, LayerMask whatIsTarget) : base(checker, whatIsTarget)
    {
        m_Distance = distance;
        m_Direction = direction;
    }

    public void OnFlip()
    {
        m_Direction.Set(-m_Direction.x, m_Direction.y);
    }

    public void CheckIfTouchingBarrier()
    {
        IsDetected = Physics2D.Raycast(Position, m_Direction, m_Distance, WhatIsTarget);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(Position, Position + m_Direction * m_Distance);
    }
}
