public class WallSlideAS : MoveAS<MoveOnWallAbility>
{
    protected override void Start()
    {
        base.Start();
        bool GrabCondition() => Entity.Behaviour.Grab && Entity.Behaviour.Move.y != -1 && Entity.AttackAbility.CanWallClimb;

        Transitions.Add(new(Ability.Grab, GrabCondition));
    }

    protected override void ApplyEnterActions()
    {
        StartSpeed = Entity.Velocity.y;
        MoveDirection = -1;
        base.ApplyEnterActions();
        Entity.RotateBodyIntoDirection(-Entity.FacingDirection);
    }

    protected override void ApplyUpdateActions()
    {
        base.ApplyUpdateActions();
        Entity.SetVelocityY(MoveSpeed);
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        Entity.RotateBodyIntoDirection(Entity.FacingDirection);
    }
}
