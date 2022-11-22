using System.Collections;
using System.Collections.Generic;

using All.Events;

using UnityEngine;

public class TargetChecker : Component, ITargetChecker, IChecker
{
    [SerializeField] private PickableColor _color;

    [SerializeField] private LayerMask _whatIsTarget;

    [SerializeField] private float _targetDetectRadius;

    public float TargetDistance
    {
        get;
        private set;
    }

    public bool TargetDetected
    {
        get;
        private set;
    }

    public Vector2 TargetPosition
    {
        get;
        private set;
    }

    public int TargetDirection
    {
        get => TargetPosition.x - transform.position.x > 0 ? 1 : -1;
    }

    private void FixedUpdate()
    {
        TargetDistance = Mathf.Abs((TargetPosition - (Vector2)transform.position).magnitude);
    }

    private void OnDrawGizmos()
    {
        Utility.DrawCircle(transform.position, _targetDetectRadius, TargetDetected, _color.Color);
    }

    public void UpdateCheckersPosition()
    {
        
    }

    public void DoChecks()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, _targetDetectRadius, _whatIsTarget);
        TargetDetected = hit;
        if (TargetDetected)
        {
            TargetPosition = hit.transform.position;
        }
    }
}
