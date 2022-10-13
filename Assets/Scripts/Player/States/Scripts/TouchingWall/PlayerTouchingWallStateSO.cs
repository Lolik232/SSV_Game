public class PlayerTouchingWallStateSO : PlayerStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => Player.isGrounded && 
														(!Player.grabInput || Player.moveInput.y < 0f);

		bool InAirCondition() => !Player.isTouchingWall ||
														 (abilities.wallJump.isActive && !abilities.wallJump.outOfWall) || 
														 (Player.moveInput.x != Player.facingDirection && !Player.grabInput);

		bool LedgeGrabCondition() => Player.isTouchingWall && 
																 !Player.isTouchingLedge && 
																 !Player.isGroundClose;

		void IdleActions()
		{
			if (!abilities.wallJump.isActive)
			{
				abilities.wallJump.StartCoyoteTime();
			}
		}

		transitions.Add(new TransitionItem(states.idle, IdleCondition));
		transitions.Add(new TransitionItem(states.inAir, InAirCondition, IdleActions));
		transitions.Add(new TransitionItem(states.ledgeGrab, LedgeGrabCondition));

		enterActions.Add(() =>
		{
			Player.MoveToX(Player.wallPosition.x + Player.wallDirection * (Player.Collider.size.x / 2 + 0.02f));
			abilities.wallJump.RestoreAmountOfUsages();
		});
	}
}
