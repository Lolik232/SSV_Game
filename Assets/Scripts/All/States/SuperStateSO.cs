using System;
using System.Collections;
using System.Collections.Generic;

using All.BaseClasses;
using All.Events;

using Unity.VisualScripting;

using UnityEngine;

public class SuperStateSO : StateBaseSO
{
    protected override void TryGetTransitionState(StateSO transitionState)
    {
        base.TryGetTransitionState(transitionState);
        OnStateExit();
    }
}
