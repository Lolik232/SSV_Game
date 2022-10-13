using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoveState", menuName = "Player/States/Grounded/Move")]
public class PlayerMoveStateSO : PlayerGroundedStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => Player.moveInput.x == 0;

		bool CrouchMoveCondition() => Player.moveInput.y < 0;

		bool LedgeClimbCondition() => Player.isTouchingWall && 
																	!Player.isTouchingLedge &&
																	Player.moveInput.x == -Player.wallDirection;

		transitions.Add(new TransitionItem(states.idle, IdleCondition));
		transitions.Add(new TransitionItem(states.crouchMove, CrouchMoveCondition));
		transitions.Add(new TransitionItem(states.ledgeClimb, LedgeClimbCondition));

		updateActions.Add(() =>
		{
			Player.CheckIfShouldFlip(Player.moveInput.x);
			Player.TrySetVelocityX(Player.moveInput.x * Player.MoveSpeed);
		});
	}
}
