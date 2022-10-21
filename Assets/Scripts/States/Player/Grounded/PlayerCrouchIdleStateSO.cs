using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCrouchIdleState", menuName = "Player/States/Grounded/Crouch Idle")]

public class PlayerCrouchIdleStateSO : PlayerGroundedStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => data.input.moveInput.y > -1 &&
														!data.checkers.isTouchingCeiling;

		bool CrouchMoveCondition() => data.input.moveInput.x != 0;

		transitions.Add(new TransitionItem(data.states.idle, IdleCondition));
		transitions.Add(new TransitionItem(data.states.crouchMove, CrouchMoveCondition));

		enterActions.Add(() =>
		{
			player.TrySetVelocityZero();
			player.Crouch();
		});

		exitActions.Add(() =>
		{
			player.Stand();
		});
	}
}
