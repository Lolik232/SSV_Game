
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SkeletonWarriorMoveToTargetBS : BehaviuorState<SkeletonWarriorBehaviour>
{
    protected void Start()
    {
        bool StayCondition() => !Entity.TargetDetected || !Entity.TouchingEdge || Entity.TouchingWall;

        bool AttackCondition() => Entity.AttackPermited;

        Transitions.Add(new(Controller.StayCommand, StayCondition));
        Transitions.Add(new(Controller.AttackCommand, AttackCondition));
    }

    protected override void ApplyUpdateActions()
    {
        base.ApplyUpdateActions();
        if (Mathf.Abs(Entity.TargetPosition.x - Entity.Position.x) > Entity.Size.x)
        {
            Controller.Move = new Vector2Int(Entity.TargetDirection, 0);
        }
        else
        {
            Controller.Move = Vector2Int.zero;
        }
    }
}
