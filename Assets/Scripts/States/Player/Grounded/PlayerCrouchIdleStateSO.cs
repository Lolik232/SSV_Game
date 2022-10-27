using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCrouchIdleState", menuName = "Player/States/Grounded/Crouch Idle")]

public class PlayerCrouchIdleStateSO : GroundedStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => entity.controller.move.y > -1 &&
														!entity.checkers.touchingCeiling;

		bool CrouchMoveCondition() => entity.controller.move.x != 0;

		transitions.Add(new TransitionItem(entity.states.idle, IdleCondition));
		transitions.Add(new TransitionItem(entity.states.crouchMove, CrouchMoveCondition));

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
