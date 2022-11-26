using System.Collections;
using System.Collections.Generic;

using All.Events;

using UnityEngine;

[RequireComponent(typeof(Physical))]

public class AttackChecker : Component, IAttackChecker, IChecker
{
    [SerializeField] private PickableColor _color;

    [SerializeField] private LayerMask _whatIsTarget;

    [SerializeField] private float _targetDetectRadius;

    private Physical _physical;

    public bool AttackPermited
    {
        get;
        private set;
    }

    private void Awake()
    {
        _physical = GetComponent<Physical>();
    }

    private void OnDrawGizmos()
    {
        Utility.DrawCircle(transform.position, _targetDetectRadius, AttackPermited, _color.Color);
    }

    public void UpdateCheckersPosition()
    {
        
    }

    public void DoChecks()
    {
        Collider2D hit = Physics2D.OverlapCircle(_physical.Center, _targetDetectRadius, _whatIsTarget);
        AttackPermited = hit;
    }
}
