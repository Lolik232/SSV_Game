using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCrouchIdleState", menuName = "Player/States/Grounded/Crouch Idle")]

public class PlayerCrouchIdleStateSO : PlayerGroundedStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => inputReader.moveInput.y > -1 &&
														!player.isTouchingCeiling;

		bool CrouchMoveCondition() => inputReader.moveInput.x != 0;

		transitions.Add(new TransitionItem(states.idle, IdleCondition));
		transitions.Add(new TransitionItem(states.crouchMove, CrouchMoveCondition));

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
