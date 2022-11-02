public class PlayerStandAbility : MoveStopAbility
{
	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		movable.TrySetVelocityX(rotateable.FacingDirection * MoveSpeed);
	}
}
