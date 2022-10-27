using UnityEditor;

public class TouchingWallStateSO : StateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => entity.checkers.grounded &&
														(!entity.controller.grab || entity.controller.move.y < 0f);

		bool LedgeGrabCondition() => entity.checkers.touchingWall &&
																 !entity.checkers.touchingLedge &&
																 !entity.checkers.groundClose;

		bool InAirCondition() => !entity.checkers.touchingWall ||
														 (entity.controller.move.x != entity.facingDirection && !entity.controller.grab);

		void InAirAction()
		{
			if (!entity.abilities.wallJump.isActive)
			{
				entity.abilities.wallJump.StartCoyoteTime();
			}
		}

		void LedgeGrabAction() => entity.checkers.DetermineLedgePosition();

		transitions.Add(new TransitionItem(entity.states.inAir, InAirCondition, InAirAction));
		transitions.Add(new TransitionItem(entity.states.idle, IdleCondition));
		transitions.Add(new TransitionItem(entity.states.ledgeGrab, LedgeGrabCondition, LedgeGrabAction));

		enterActions.Add(() =>
		{
			entity.HoldDirection(-entity.checkers.wallDirection);
			entity.MoveToX(entity.checkers.wallPosition.x + entity.checkers.wallDirection * (entity.Size.x / 2 + 0.02f));
			entity.abilities.wallJump.RestoreAmountOfUsages();
			entity.abilities.jump.SetAmountOfUsagesToZero();
			entity.abilities.attack.HoldDirection(entity.checkers.wallDirection);
		});

		exitActions.Add(() =>
		{
			entity.ReleaseDirection();
			entity.RotateIntoDirection(entity.realDirection);
			entity.abilities.attack.ReleaseDirection();
		});
	}
}
