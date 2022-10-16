public class PlayerTouchingWallStateSO : PlayerStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => player.isGrounded &&
														(!inputReader.grabInput || inputReader.moveInput.y < 0f);

		bool InAirCondition() => !player.isTouchingWall ||
														 (inputReader.moveInput.x != player.facingDirection && !inputReader.grabInput);

		bool LedgeGrabCondition() => player.isTouchingWall &&
																 !player.isTouchingLedge &&
																 !player.isGroundClose;

		void IdleAction()
		{
			if (!abilities.wallJump.isActive)
			{
				abilities.wallJump.StartCoyoteTime();
			}
		}

		void LedgeGrabAction() => player.DetermineLedgePosition();

		transitions.Add(new TransitionItem(states.idle, IdleCondition));
		transitions.Add(new TransitionItem(states.inAir, InAirCondition, IdleAction));
		transitions.Add(new TransitionItem(states.ledgeGrab, LedgeGrabCondition, LedgeGrabAction));

		enterActions.Add(() =>
		{
			player.MoveToX(player.wallPosition.x + player.wallDirection * (player.col.size.x / 2 + 0.02f));
			abilities.wallJump.RestoreAmountOfUsages();
			abilities.jump.SetAmountOfUsagesToZero();
		});
	}
}
