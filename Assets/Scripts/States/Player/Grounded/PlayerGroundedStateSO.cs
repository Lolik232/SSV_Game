public class PlayerGroundedStateSO : PlayerStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool InAirCondition() => !data.checkers.grounded;

		bool WallGrabCondition() => data.checkers.touchingWall &&
																data.checkers.touchingLedge &&
																!data.checkers.touchingCeiling &&
																data.input.grabInput &&
																data.input.moveInput.y >= 0f;

		void InAirActions()
		{
			if (data.checkers.touchingCeiling && !entity.isStanding)
			{
				entity.MoveToY(entity.Position.y - (entity.StandSize.y - entity.CrouchSize.y));
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
