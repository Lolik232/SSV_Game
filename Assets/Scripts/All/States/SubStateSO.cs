using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SubStateSO : StateSO
{
    [SerializeField] private SuperStateSO _superState;

    protected override void OnEnable()
    {
        base.OnEnable();
        enterActions.Add(() => { _superState.OnStateEnter(); });
        updateActions.Add(() => { _superState.OnUpdate(); });
        checks.Add(() => { _superState.OnFixedUpdate(); });
    }
}
