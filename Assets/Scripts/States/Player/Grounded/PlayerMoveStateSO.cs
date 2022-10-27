using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoveState", menuName = "Player/States/Grounded/Move")]
public class PlayerMoveStateSO : GroundedStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => entity.controller.move.x == 0;

		bool CrouchMoveCondition() => entity.controller.move.y < 0;

		bool LedgeClimbCondition() => entity.checkers.touchingWall &&
																	!entity.checkers.touchingLedge &&
																	entity.controller.move.x == -entity.checkers.wallDirection;

		void LedgeClimbAction() => entity.checkers.DetermineLedgePosition();

		transitions.Add(new TransitionItem(entity.states.idle, IdleCondition));
		transitions.Add(new TransitionItem(entity.states.crouchMove, CrouchMoveCondition));
		transitions.Add(new TransitionItem(entity.states.ledgeClimb, LedgeClimbCondition, LedgeClimbAction));

		updateActions.Add(() =>
		{
			entity.TryRotateIntoDirection(entity.controller.move.x);
			entity.TrySetVelocityX(entity.controller.move.x * entity.parameters.moveSpeed);
		});
	}
}
