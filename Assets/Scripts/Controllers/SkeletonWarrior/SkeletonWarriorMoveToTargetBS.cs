﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SkeletonWarriorMoveToTargetBS : BehaviuorState<SkeletonWarriorBehaviour>
{
    protected void Start()
    {
        bool StayCondition() => !Entity.TargetDetected;

        bool AttackCondition() => Entity.TargetDistance < ((MeleeWeapon)Entity.Inventory.Current).Length;

        Transitions.Add(new(Controller.StayCommand, StayCondition));
        Transitions.Add(new(Controller.AttackCommand, AttackCondition));
    }

    protected override void ApplyUpdateActions()
    {
        base.ApplyUpdateActions();
        if (Mathf.Abs(Entity.Position.x - Entity.TargetPosition.x) > Entity.Size.x && Entity.TouchingEdge && !Entity.TouchingWall)
        {
            Controller.Move = new Vector2Int(Entity.TargetDirection, 0);
        }
        else
        {
            Controller.Move = Vector2Int.zero;
        }
    }
}