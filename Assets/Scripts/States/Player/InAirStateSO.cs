using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInAirState", menuName = "Player/States/In Air")]
public class InAirStateSO : StateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool LandCondition() => entity.checkers.grounded &&
														entity.Velocity.y < 0.01f;

		bool LedgeGrabCondition() => entity.checkers.touchingWall &&
																 !entity.checkers.touchingLedge &&
																 !entity.checkers.groundClose;

		bool WallGrabCondition() => entity.checkers.touchingWall &&
																entity.checkers.touchingLedge &&
																entity.controller.grab;

		bool WallSlideCondition() => entity.checkers.touchingWall &&
																 entity.controller.move.x == entity.facingDirection &&
																 entity.Velocity.y <= 0f;

		void LedgeGrabAction() => entity.checkers.DetermineLedgePosition();

		transitions.Add(new TransitionItem(entity.states.land, LandCondition));
		transitions.Add(new TransitionItem(entity.states.ledgeGrab, LedgeGrabCondition, LedgeGrabAction));
		transitions.Add(new TransitionItem(entity.states.wallGrab, WallGrabCondition));
		transitions.Add(new TransitionItem(entity.states.wallSlide, WallSlideCondition));

		enterActions.Add(() =>
		{
			CheckForJumps();
		});

		updateActions.Add(() =>
		{
			CheckForJumps();

			entity.TrySetVelocityX(entity.controller.move.x * entity.parameters.inAirMoveSpeed);
			entity.TryRotateIntoDirection(entity.controller.move.x);

			anim.SetFloat("xVelocity", entity.Velocity.x);
			anim.SetFloat("yVelocity", entity.Velocity.y);
		});
	}

	private void CheckForJumps()
	{
		if (!entity.abilities.jump.IsCoyoteTime())
		{
			entity.abilities.jump.SetAmountOfUsagesToZero();
		}

		if (entity.checkers.touchingWall || entity.checkers.touchingWallBack)
		{
			entity.abilities.wallJump.RestoreAmountOfUsages();
		}
		else if (!entity.abilities.wallJump.IsCoyoteTime())
		{
			entity.abilities.wallJump.SetAmountOfUsagesToZero();
		}
	}
}
