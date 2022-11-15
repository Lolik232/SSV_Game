using UnityEngine;

[RequireComponent(typeof(GroundChecker))]

public class DummyInAirState : State
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
        bool GroundedCondition() => _groundChecker.Grounded && _dummy.Velocity.y < 0.01f;

        Transitions.Add(new(_dummy.GroundedState, GroundedCondition));
    }
}
