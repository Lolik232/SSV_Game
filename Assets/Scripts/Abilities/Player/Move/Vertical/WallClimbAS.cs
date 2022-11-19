public class WallClimbAS : MoveAS<MoveOnWallAbility>
{
    protected override void Start()
    {
        base.Start();
        bool GrabCondition() => !Entity.Behaviour.Grab || Entity.Behaviour.Move.y != 1;
        bool SlideCondition() => Entity.Behaviour.Attack;

        Transitions.Add(new(Ability.Grab, GrabCondition));
        Transitions.Add(new(Ability.Slide, SlideCondition));
    }

    protected override void ApplyEnterActions()
    {
        StartSpeed = Entity.Velocity.y;
        MoveDirection = 1;
        base.ApplyEnterActions();
    }

    protected override void ApplyUpdateActions()
    {
        base.ApplyUpdateActions();
        Entity.SetVelocityY(MoveSpeed);
    }
}
