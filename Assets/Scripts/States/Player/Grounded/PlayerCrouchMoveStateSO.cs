using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCrouchMoveState", menuName = "Player/States/Grounded/Crouch Move")]

public class PlayerCrouchMoveStateSO : PlayerGroundedStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool MoveCondition() => data.controller.move.y > -1 &&
														!data.checkers.touchingCeiling;

		bool CrouchIdleCondition() => data.controller.move.x == 0;

		transitions.Add(new TransitionItem(data.states.move, MoveCondition));
		transitions.Add(new TransitionItem(data.states.crouchIdle, CrouchIdleCondition));

		enterActions.Add(() =>
		{
			entity.Crouch();
		});

		updateActions.Add(() =>
		{
			entity.TryRotateIntoDirection(data.controller.move.x);
			entity.TrySetVelocityX(data.controller.move.x * data.parameters.crouchMoveSpeed);
		});

		exitActions.Add(() =>
		{
			entity.Stand();
		});
	}
}
