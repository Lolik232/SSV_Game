using UnityEngine;

public class DummyGroundedState : DummyState
{
    private void Start()
    {
        bool InAirCondition() => !Dummy.Grounded;

        Transitions.Add(new(Dummy.InAirState, InAirCondition));
    }
}
