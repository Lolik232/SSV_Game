public class PlayerGroundedStateSO : PlayerStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool InAirCondition() => !player.isGrounded;

		bool WallGrabCondition() => player.isTouchingWall &&
																player.isTouchingLedge &&
																!player.isTouchingCeiling &&
																inputReader.grabInput &&
																inputReader.moveInput.y >= 0f;

		void InAirActions()
		{
			if (player.isTouchingCeiling)
			{
				player.MoveToY(player.transform.position.y - (player.StandSize.y - player.CrouchSize.y));
			}

			if (!abilities.jump.isActive)
			{
				abilities.jump.StartCoyoteTime();
			}
		}

		transitions.Add(new TransitionItem(states.inAir, InAirCondition, InAirActions));
		transitions.Add(new TransitionItem(states.wallGrab, WallGrabCondition));

		enterActions.Add(() =>
		{
			abilities.dash.RestoreAmountOfUsages();
			abilities.jump.RestoreAmountOfUsages();
		});
	}
}
