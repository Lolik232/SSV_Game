using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInAirState", menuName = "Player/States/In Air")]
public class PlayerInAirStateSO : PlayerStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool LandCondition() => data.checkers.isGrounded &&
														player.Velocity.y < 0.01f;

		bool LedgeGrabCondition() => data.checkers.isTouchingWall &&
																 !data.checkers.isTouchingLedge &&
																 !data.checkers.isGroundClose;

		bool WallGrabCondition() => data.checkers.isTouchingWall &&
																data.checkers.isTouchingLedge &&
																data.input.grabInput;

		bool WallSlideCondition() => data.checkers.isTouchingWall &&
																 data.input.moveInput.x == player.facingDirection &&
																 player.Velocity.y <= 0f;

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

			player.TrySetVelocityX(data.input.moveInput.x * data.parameters.inAirMoveSpeed);
			player.CheckIfShouldFlip(data.input.moveInput.x);

			anim.SetFloat("xVelocity", player.Velocity.x);
			anim.SetFloat("yVelocity", player.Velocity.y);
		});
	}

	private void CheckForJumps()
	{
		if (!data.abilities.jump.IsCoyoteTime())
		{
			data.abilities.jump.SetAmountOfUsagesToZero();
		}

		if (data.checkers.isTouchingWall || data.checkers.isTouchingWallBack)
		{
			data.abilities.wallJump.RestoreAmountOfUsages();
		}
		else if (!data.abilities.wallJump.IsCoyoteTime())
		{
			data.abilities.wallJump.SetAmountOfUsagesToZero();
		}
	}
}
