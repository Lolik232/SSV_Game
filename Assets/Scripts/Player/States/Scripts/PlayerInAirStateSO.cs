using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInAirState", menuName = "Player/States/In Air")]
public class PlayerInAirStateSO : PlayerStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool LandCondition() => Player.isGrounded && 
													  Player.Rb.velocity.y < 0.01f;

		bool LedgeGrabCondition() => Player.isTouchingWall && 
																 !Player.isTouchingLedge && 
																 !Player.isGroundClose;

		bool WallGrabCondition() => (!abilities.wallJump.isActive || abilities.wallJump.outOfWall) && 
																Player.isTouchingWall &&
																Player.isTouchingLedge &&
																Player.grabInput;

		bool WallSlideCondition() => (!abilities.wallJump.isActive || abilities.wallJump.outOfWall) && 
																 Player.isTouchingWall && 
																 Player.moveInput.x == Player.facingDirection && 
																 Player.Rb.velocity.y <= 0f;

		transitions.Add(new TransitionItem(states.land, LandCondition));
		transitions.Add(new TransitionItem(states.ledgeGrab, LedgeGrabCondition));
		transitions.Add(new TransitionItem(states.wallGrab, WallGrabCondition));
		transitions.Add(new TransitionItem(states.wallSlide, WallSlideCondition));

		updateActions.Add(() =>
		{
			if (!abilities.jump.CoyoteTime)
			{
				abilities.jump.SetAmountOfUsagesToZero();
			}

			if (Player.isTouchingWall || Player.isTouchingWallBack)
			{
				abilities.wallJump.RestoreAmountOfUsages();
			}
			else if (!abilities.wallJump.CoyoteTime)
			{
				abilities.wallJump.SetAmountOfUsagesToZero();
			}

			Player.TrySetVelocityX(Player.moveInput.x * Player.InAirMoveSpeed);
			Player.CheckIfShouldFlip(Player.moveInput.x);

			Anim.SetFloat("xVelocity", Player.Rb.velocity.x);
			Anim.SetFloat("yVelocity", Player.Rb.velocity.y);
		});
	}
}
