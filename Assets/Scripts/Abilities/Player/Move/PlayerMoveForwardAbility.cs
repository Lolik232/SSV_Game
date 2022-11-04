public class PlayerMoveForwardAbility : MoveAbility
{
	protected override void ApplyPrepareActions()
	{
		base.ApplyPrepareActions();
		startSpeed = movable.Velocity.x;
		moveDirection = rotateable.FacingDirection;
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		movable.SetVelocityX(moveSpeed);
	}
}
