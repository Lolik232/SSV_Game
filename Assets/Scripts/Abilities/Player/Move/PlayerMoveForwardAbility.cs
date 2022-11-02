public class PlayerMoveForwardAbility : MoveAbility
{
	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		movable.TrySetVelocityX(rotateable.FacingDirection * MoveSpeed);
	}
}
