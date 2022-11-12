public class PlayerWallSlideAS : MoveAS<PlayerMoveVerticalAbility>
{
	private void Start()
	{
		bool GrabCondition() => Ability.Player.Input.Grab && Ability.Player.Input.Move.y != -1;

		Transitions.Add(new(Ability.Grab, GrabCondition));
	}

	protected override void ApplyEnterActions()
	{
		StartSpeed = Ability.Player.Velocity.y;
		MoveDirection = -1;
		base.ApplyEnterActions();
		Ability.Player.RotateBodyIntoDirection(-Ability.Player.FacingDirection);
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		Ability.Player.SetVelocityY(MoveSpeed);
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		Ability.Player.RotateBodyIntoDirection(Ability.Player.FacingDirection);
	}
}
