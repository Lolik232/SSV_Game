using System.Collections;
using System.Collections.Generic;

using All.Events;

using UnityEngine;

[RequireComponent(typeof(Physical))]
public class TargetChecker : Component, ITargetChecker, IChecker
{
    [SerializeField] private PickableColor _color;

    [SerializeField] private LayerMask _whatIsTarget;

    [SerializeField] private float _targetDetectRadius;

    private Physical _physical;

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

    private void Awake()
    {
        _physical = GetComponent<Physical>();
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
        var hits = Physics2D.OverlapCircleAll(_physical.Center, _targetDetectRadius, _whatIsTarget);
        if (TargetDetected = hits.Length > 0)
        {
            Collider2D nearest = hits[0];
            foreach (var hit in hits)
            {
                if ((hit.transform.position - transform.position).magnitude < (nearest.transform.position - transform.position).magnitude)
                {
                    nearest = hit;
                }
            }

            if (TargetDetected)
            {
                if (nearest.GetComponent<Entity>() is IPhysical physical)
                {
                    TargetPosition = physical.Center;
                }
                else
                {
                    TargetPosition = nearest.transform.position;
                }
            }
        }
    }
}
