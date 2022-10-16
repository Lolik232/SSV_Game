using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInAirState", menuName = "Player/States/In Air")]
public class PlayerInAirStateSO : PlayerStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool LandCondition() => player.isGrounded &&
														player.rb.velocity.y < 0.01f;

		bool LedgeGrabCondition() => player.isTouchingWall &&
																 !player.isTouchingLedge &&
																 !player.isGroundClose;

		bool WallGrabCondition() => player.isTouchingWall &&
																player.isTouchingLedge &&
																inputReader.grabInput;

		bool WallSlideCondition() => player.isTouchingWall &&
																 inputReader.moveInput.x == player.facingDirection &&
																 player.rb.velocity.y <= 0f;

		void LedgeGrabAction() => player.DetermineLedgePosition();

		transitions.Add(new TransitionItem(states.land, LandCondition));
		transitions.Add(new TransitionItem(states.ledgeGrab, LedgeGrabCondition, LedgeGrabAction));
		transitions.Add(new TransitionItem(states.wallGrab, WallGrabCondition));
		transitions.Add(new TransitionItem(states.wallSlide, WallSlideCondition));

		enterActions.Add(()=>
		{
			CheckForJumps();
		});

		updateActions.Add(() =>
		{
			CheckForJumps();

			player.TrySetVelocityX(inputReader.moveInput.x * parameters.inAirMoveSpeed);
			player.CheckIfShouldFlip(inputReader.moveInput.x);

			anim.SetFloat("xVelocity", player.rb.velocity.x);
			anim.SetFloat("yVelocity", player.rb.velocity.y);
		});
	}

	private void CheckForJumps()
	{
		if (!abilities.jump.IsCoyoteTime())
		{
			abilities.jump.SetAmountOfUsagesToZero();
		}

		if (player.isTouchingWall || player.isTouchingWallBack)
		{
			abilities.wallJump.RestoreAmountOfUsages();
		}
		else if (!abilities.wallJump.IsCoyoteTime())
		{
			abilities.wallJump.SetAmountOfUsagesToZero();
		}
	}
}
