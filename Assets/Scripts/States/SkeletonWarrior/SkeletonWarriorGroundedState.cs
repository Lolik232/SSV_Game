using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorGroundedState : SkeletonWarriorState
{
    protected override void Start()
    {
        base.Start();
        bool InAirCondition() => !Skeleton.Grounded;

        Transitions.Add(new(Skeleton.InAirState, InAirCondition));
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        Skeleton.MoveHorizontalAbility.Permited = true;
    }
}
