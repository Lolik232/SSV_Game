using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCrouchIdleState", menuName = "Player/States/Grounded/Crouch Idle")]

public class PlayerCrouchIdleStateSO : PlayerGroundedStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => data.controller.move.y > -1 &&
														!data.checkers.touchingCeiling;

		bool CrouchMoveCondition() => data.controller.move.x != 0;

		transitions.Add(new TransitionItem(data.states.idle, IdleCondition));
		transitions.Add(new TransitionItem(data.states.crouchMove, CrouchMoveCondition));

		enterActions.Add(() =>
		{
			entity.TrySetVelocityZero();
			entity.Crouch();
		});

		exitActions.Add(() =>
		{
			entity.Stand();
		});
	}
}
