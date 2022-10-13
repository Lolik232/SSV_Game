public class PlayerGroundedStateSO : PlayerStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool InAirCondition() => !Player.isGrounded ||
														 abilities.jump.isActive ||
														 abilities.dash.isActive;

		bool WallGrabCondition() => Player.isTouchingWall && 
																Player.isTouchingLedge && 
																!Player.isTouchingCeiling && 
																Player.grabInput && 
																Player.moveInput.y >= 0f;

		void InAirActions()
		{
			if (Player.isTouchingCeiling)
			{
				Player.MoveToY(Player.transform.position.y - (Player.StandSize.y - Player.CrouchSize.y));
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
