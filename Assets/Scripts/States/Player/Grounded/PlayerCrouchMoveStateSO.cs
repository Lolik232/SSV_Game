using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCrouchMoveState", menuName = "Player/States/Grounded/Crouch Move")]

public class PlayerCrouchMoveStateSO : GroundedStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool MoveCondition() => entity.controller.move.y > -1 &&
														!entity.checkers.touchingCeiling;

		bool CrouchIdleCondition() => entity.controller.move.x == 0;

		transitions.Add(new TransitionItem(entity.states.move, MoveCondition));
		transitions.Add(new TransitionItem(entity.states.crouchIdle, CrouchIdleCondition));

		enterActions.Add(() =>
		{
			entity.Crouch();
		});

		updateActions.Add(() =>
		{
			entity.TryRotateIntoDirection(entity.controller.move.x);
			entity.TrySetVelocityX(entity.controller.move.x * entity.parameters.crouchMoveSpeed);
		});

		exitActions.Add(() =>
		{
			entity.Stand();
		});
	}
}
