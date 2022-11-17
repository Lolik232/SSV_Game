using UnityEngine;

public class DummyInAirState : DummyState
{
    private void Start()
    {
        bool GroundedCondition() => Dummy.Grounded && Dummy.Velocity.y < 0.01f;

        Transitions.Add(new(Dummy.GroundedState, GroundedCondition));
    }
}
