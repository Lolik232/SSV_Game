using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(GroundChecker))]

public class DummyGroundedState : State
{
    private Dummy _dummy;

    private GroundChecker _groundChecker;

    protected override void Awake()
    {
        base.Awake();
        _dummy = GetComponent<Dummy>();

        Checkers.Add(_groundChecker = GetComponent<GroundChecker>());
    }

    private void Start()
    {
        bool InAirCondition() => !_groundChecker.Grounded;

        Transitions.Add(new(_dummy.InAirState, InAirCondition));
    }
}
