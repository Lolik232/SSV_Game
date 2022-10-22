using UnityEditor;

public class PlayerTouchingWallStateSO : PlayerStateSO
{
	protected bool needHadFlip;

	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => data.checkers.grounded &&
														(!data.input.grabInput || data.input.moveInput.y < 0f);

		bool LedgeGrabCondition() => data.checkers.touchingWall &&
																 !data.checkers.touchingLedge &&
																 !data.checkers.groundClose;

		bool InAirCondition() => !data.checkers.touchingWall ||
														 (data.input.moveInput.x != entity.facingDirection && !data.input.grabInput);

		void InAirAction()
		{
			if (needHadFlip)
			{
				entity.HardFlip();
			}

			if (!data.abilities.wallJump.isActive)
			{
				data.abilities.wallJump.StartCoyoteTime();
			}
		}

		void LedgeGrabAction() => data.checkers.DetermineLedgePosition();

		transitions.Add(new TransitionItem(data.states.inAir, InAirCondition, InAirAction));
		transitions.Add(new TransitionItem(data.states.idle, IdleCondition));
		transitions.Add(new TransitionItem(data.states.ledgeGrab, LedgeGrabCondition, LedgeGrabAction));

		enterActions.Add(() =>
		{
			needHadFlip = false;
			entity.MoveToX(data.checkers.wallPosition.x + data.checkers.wallDirection * (entity.Size.x / 2 + 0.02f));
			data.abilities.wallJump.RestoreAmountOfUsages();
			data.abilities.jump.SetAmountOfUsagesToZero();
			data.abilities.attack.HoldDirection(data.checkers.wallDirection);
		});

		exitActions.Add(() =>
		{
			data.abilities.attack.ReleaseDirection();
		});
	}
}
