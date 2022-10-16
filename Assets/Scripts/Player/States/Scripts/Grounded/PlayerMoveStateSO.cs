using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoveState", menuName = "Player/States/Grounded/Move")]
public class PlayerMoveStateSO : PlayerGroundedStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => inputReader.moveInput.x == 0;

		bool CrouchMoveCondition() => inputReader.moveInput.y < 0;

		bool LedgeClimbCondition() => player.isTouchingWall &&
																	!player.isTouchingLedge &&
																	inputReader.moveInput.x == -player.wallDirection;

		void LedgeClimbAction() => player.DetermineLedgePosition();

		transitions.Add(new TransitionItem(states.idle, IdleCondition));
		transitions.Add(new TransitionItem(states.crouchMove, CrouchMoveCondition));
		transitions.Add(new TransitionItem(states.ledgeClimb, LedgeClimbCondition, LedgeClimbAction));

		updateActions.Add(() =>
		{
			player.CheckIfShouldFlip(inputReader.moveInput.x);
			player.TrySetVelocityX(inputReader.moveInput.x * parameters.moveSpeed);
		});
	}
}
