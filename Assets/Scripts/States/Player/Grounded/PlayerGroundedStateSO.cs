public class PlayerGroundedStateSO : PlayerStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool InAirCondition() => !data.checkers.isGrounded;

		bool WallGrabCondition() => data.checkers.isTouchingWall &&
																data.checkers.isTouchingLedge &&
																!data.checkers.isTouchingCeiling &&
																data.input.grabInput &&
																data.input.moveInput.y >= 0f;

		void InAirActions()
		{
			if (data.checkers.isTouchingCeiling && !player.isStanding)
			{
				player.MoveToY(player.Position.y - (player.StandSize.y - player.CrouchSize.y));
			}

			if (!data.abilities.jump.isActive)
			{
				data.abilities.jump.StartCoyoteTime();
			}
		}

		transitions.Add(new TransitionItem(data.states.inAir, InAirCondition, InAirActions));
		transitions.Add(new TransitionItem(data.states.wallGrab, WallGrabCondition));

		enterActions.Add(() =>
		{
			data.abilities.dash.RestoreAmountOfUsages();
			data.abilities.jump.RestoreAmountOfUsages();
		});
	}
}
