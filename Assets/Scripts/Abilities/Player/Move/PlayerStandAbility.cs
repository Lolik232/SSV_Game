using UnityEngine;

public class PlayerStandAbility : MoveStopAbility
{
	protected override void ApplyPrepareActions()
	{
		base.ApplyPrepareActions();
		startSpeed = movable.Velocity.x;
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		movable.SetVelocityX(moveSpeed);
	}
}
