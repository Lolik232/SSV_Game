public class PlayerWallClimbAS : MoveAS<PlayerMoveVerticalAbility>
{
    private void Start()
    {
        bool GrabCondition() => !Ability.Player.Input.Grab || Ability.Player.Input.Move.y != 1; 
        bool SlideCondition() => Ability.Player.Input.Attack;

        Transitions.Add(new(Ability.Grab, GrabCondition));
        Transitions.Add(new(Ability.Slide, SlideCondition));
    }

    protected override void ApplyEnterActions()
    {
        StartSpeed = Ability.Player.Velocity.y;
        MoveDirection = 1;
        base.ApplyEnterActions();
    }

    protected override void ApplyUpdateActions()
    {
        base.ApplyUpdateActions();
        Ability.Player.SetVelocityY(MoveSpeed);
    }
}
