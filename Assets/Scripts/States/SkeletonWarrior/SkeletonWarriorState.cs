using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorState : State
{
    protected SkeletonWarrior Skeleton
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
        Skeleton = GetComponent<SkeletonWarrior>();
    }
}
