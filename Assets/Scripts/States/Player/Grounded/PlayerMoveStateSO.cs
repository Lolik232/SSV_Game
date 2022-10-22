using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoveState", menuName = "Player/States/Grounded/Move")]
public class PlayerMoveStateSO : PlayerGroundedStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => data.input.moveInput.x == 0;

		bool CrouchMoveCondition() => data.input.moveInput.y < 0;

		bool LedgeClimbCondition() => data.checkers.touchingWall &&
																	!data.checkers.touchingLedge &&
																	data.input.moveInput.x == -data.checkers.wallDirection;

		void LedgeClimbAction() => data.checkers.DetermineLedgePosition();

		transitions.Add(new TransitionItem(data.states.idle, IdleCondition));
		transitions.Add(new TransitionItem(data.states.crouchMove, CrouchMoveCondition));
		transitions.Add(new TransitionItem(data.states.ledgeClimb, LedgeClimbCondition, LedgeClimbAction));

		updateActions.Add(() =>
		{
			entity.CheckIfShouldFlip(data.input.moveInput.x);
			entity.TrySetVelocityX(data.input.moveInput.x * data.parameters.moveSpeed);
		});
	}
}
