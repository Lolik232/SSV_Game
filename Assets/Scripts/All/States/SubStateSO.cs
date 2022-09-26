using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SubStateSO : StateSO
{
    [SerializeField] private SuperStateSO _superState;
    public SuperStateSO SuperState => _superState;

    public override void OnStateEnter()
    {
        if (!_superState.IsActive)
        {
            _superState.OnStateEnter();
        }
        base.OnStateEnter();
    }

    public override void OnUpdate()
    {
        _superState.OnUpdate();
        base.OnUpdate();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _superState.Add(this);
    }
}
