public class MoveForwardAS : MoveAS<MoveHorizontalAbility>
{
    protected override void Start()
    {
        base.Start();
        bool StayCondition() => Entity.Behaviour.Move.x != Entity.FacingDirection;

        Transitions.Add(new(Ability.Stay, StayCondition));
    }

    protected override void ApplyEnterActions()
    {
        StartSpeed = Entity.Velocity.x;
        MoveDirection = Entity.FacingDirection;
        base.ApplyEnterActions();
    }

    protected override void ApplyUpdateActions()
    {
        base.ApplyUpdateActions();
        Entity.SetVelocityX(MoveSpeed);
    }
}
