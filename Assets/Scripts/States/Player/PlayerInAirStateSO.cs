using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInAirState", menuName = "Player/States/In Air")]
public class PlayerInAirStateSO : PlayerStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool LandCondition() => data.checkers.grounded &&
														entity.Velocity.y < 0.01f;

		bool LedgeGrabCondition() => data.checkers.touchingWall &&
																 !data.checkers.touchingLedge &&
																 !data.checkers.groundClose;

		bool WallGrabCondition() => data.checkers.touchingWall &&
																data.checkers.touchingLedge &&
																data.controller.grab;

		bool WallSlideCondition() => data.checkers.touchingWall &&
																 data.controller.move.x == entity.facingDirection &&
																 entity.Velocity.y <= 0f;

		void LedgeGrabAction() => data.checkers.DetermineLedgePosition();

		transitions.Add(new TransitionItem(data.states.land, LandCondition));
		transitions.Add(new TransitionItem(data.states.ledgeGrab, LedgeGrabCondition, LedgeGrabAction));
		transitions.Add(new TransitionItem(data.states.wallGrab, WallGrabCondition));
		transitions.Add(new TransitionItem(data.states.wallSlide, WallSlideCondition));

		enterActions.Add(() =>
		{
			CheckForJumps();
		});

		updateActions.Add(() =>
		{
			CheckForJumps();

			entity.TrySetVelocityX(data.controller.move.x * data.parameters.inAirMoveSpeed);
			entity.TryRotateIntoDirection(data.controller.move.x);

			anim.SetFloat("xVelocity", entity.Velocity.x);
			anim.SetFloat("yVelocity", entity.Velocity.y);
		});
	}

	private void CheckForJumps()
	{
		if (!data.abilities.jump.IsCoyoteTime())
		{
			data.abilities.jump.SetAmountOfUsagesToZero();
		}

		if (data.checkers.touchingWall || data.checkers.touchingWallBack)
		{
			data.abilities.wallJump.RestoreAmountOfUsages();
		}
		else if (!data.abilities.wallJump.IsCoyoteTime())
		{
			data.abilities.wallJump.SetAmountOfUsagesToZero();
		}
	}
}
