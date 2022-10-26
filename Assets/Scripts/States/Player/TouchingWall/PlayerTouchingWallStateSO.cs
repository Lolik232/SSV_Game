using UnityEditor;

public class PlayerTouchingWallStateSO : PlayerStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => data.checkers.grounded &&
														(!data.controller.grab || data.controller.move.y < 0f);

		bool LedgeGrabCondition() => data.checkers.touchingWall &&
																 !data.checkers.touchingLedge &&
																 !data.checkers.groundClose;

		bool InAirCondition() => !data.checkers.touchingWall ||
														 (data.controller.move.x != entity.facingDirection && !data.controller.grab);

		void InAirAction()
		{
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
			entity.HoldDirection(-data.checkers.wallDirection);
			entity.MoveToX(data.checkers.wallPosition.x + data.checkers.wallDirection * (entity.Size.x / 2 + 0.02f));
			data.abilities.wallJump.RestoreAmountOfUsages();
			data.abilities.jump.SetAmountOfUsagesToZero();
			data.abilities.attack.HoldDirection(data.checkers.wallDirection);
		});

		exitActions.Add(() =>
		{
			entity.ReleaseDirection();
			entity.RotateIntoDirection(entity.realDirection);
			data.abilities.attack.ReleaseDirection();
		});
	}
}
