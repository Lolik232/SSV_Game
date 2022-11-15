using UnityEngine;

public class DummyGroundedState : State
{
    private Dummy _dummy;

    protected override void Awake()
    {
        base.Awake();
        _dummy = GetComponent<Dummy>();
    }

    private void Start()
    {
        bool InAirCondition() => !_dummy.Grounded;

        Transitions.Add(new(_dummy.InAirState, InAirCondition));
    }
}
