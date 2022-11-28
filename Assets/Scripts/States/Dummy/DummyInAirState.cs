public class DummyInAirState : DummyState
{
    protected override void Start()
    {
        base.Start();
        bool GroundedCondition() => Dummy.Grounded && Dummy.Velocity.y < 0.01f;

        Transitions.Add(new(Dummy.GroundedState, GroundedCondition));
    }
}
