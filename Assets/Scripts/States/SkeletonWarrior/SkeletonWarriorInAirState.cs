using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorInAirState : SkeletonWarriorState
{
    protected override void Start()
    {
        base.Start();
        bool GroundedCondition() => Skeleton.Grounded && Skeleton.Velocity.y < 0.01f;

        Transitions.Add(new(Skeleton.GroundedState, GroundedCondition));
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        Skeleton.MoveHorizontalAbility.Permited = false;
    }
}
