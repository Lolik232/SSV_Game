using System;

using UnityEngine;

public class BarrierChecker : EnvironmentChecker
{
    public readonly Single Distance;

    private Int32 m_FacingDirection;

    private readonly Vector2 m_Direction;
    public Vector2 Direction => new(m_Direction.x * m_FacingDirection, m_Direction.y);

    public BarrierChecker(Transform checker, Single distance, Vector2 direction, LayerMask whatIsTarget) : base(checker, whatIsTarget)
    {
        Distance = distance;
        m_Direction = direction;
    }

    public void OnFacingDirectionChanged(Int32 direction)
    {
        m_FacingDirection = direction;
    }

    public void CheckIfTouchingBarrier()
    {
        IsDetected = Physics2D.Raycast(Position, Direction, Distance, WhatIsTarget);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(Position, Position + Direction * Distance);
    }
}
