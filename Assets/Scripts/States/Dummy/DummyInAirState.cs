using UnityEngine;

public class DummyInAirState : State
{
    private Dummy _dummy;

    protected override void Awake()
    {
        base.Awake();
        _dummy = GetComponent<Dummy>();
    }

    private void Start()
    {
        bool GroundedCondition() => _dummy.Grounded && _dummy.Velocity.y < 0.01f;

        Transitions.Add(new(_dummy.GroundedState, GroundedCondition));
    }
}
