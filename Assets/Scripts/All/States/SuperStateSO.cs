using System;
using System.Collections;
using System.Collections.Generic;

using All.BaseClasses;
using All.Events;

using Unity.VisualScripting;

using UnityEngine;

public class SuperStateSO : StateSO
{
    public bool IsActive => isActive;

    protected List<SubStateSO> subStates = new();
    public void Add(SubStateSO subState) => subStates.Add(subState);

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        subStates.Clear();
    }

    protected override void TryGetTransitionState(StateSO transitionState)
    {
        base.TryGetTransitionState(transitionState);
        OnStateExit();
    }
}
