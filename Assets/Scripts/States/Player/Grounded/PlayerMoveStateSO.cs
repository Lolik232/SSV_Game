using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoveState", menuName = "Player/States/Grounded/Move")]
public class PlayerMoveStateSO : PlayerGroundedStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => data.controller.move.x == 0;

		bool CrouchMoveCondition() => data.controller.move.y < 0;

		bool LedgeClimbCondition() => data.checkers.touchingWall &&
																	!data.checkers.touchingLedge &&
																	data.controller.move.x == -data.checkers.wallDirection;

		void LedgeClimbAction() => data.checkers.DetermineLedgePosition();

		transitions.Add(new TransitionItem(data.states.idle, IdleCondition));
		transitions.Add(new TransitionItem(data.states.crouchMove, CrouchMoveCondition));
		transitions.Add(new TransitionItem(data.states.ledgeClimb, LedgeClimbCondition, LedgeClimbAction));

		updateActions.Add(() =>
		{
			entity.TryRotateIntoDirection(data.controller.move.x);
			entity.TrySetVelocityX(data.controller.move.x * data.parameters.moveSpeed);
		});
	}
}
