public class DummyGroundedState : DummyState
{
    protected override void Start()
    {
        base.Start();
        bool InAirCondition() => !Dummy.Grounded;

        Transitions.Add(new(Dummy.InAirState, InAirCondition));
    }
}
