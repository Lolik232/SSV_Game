using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCrouchIdleState", menuName = "Player/States/Grounded/Crouch Idle")]

public class PlayerCrouchIdleStateSO : PlayerGroundedStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => Player.moveInput.y > -1 && 
														!Player.isTouchingCeiling;

		bool CrouchMoveCondition() => Player.moveInput.x != 0;

		transitions.Add(new TransitionItem(states.idle, IdleCondition));
		transitions.Add(new TransitionItem(states.crouchMove, CrouchMoveCondition));

		enterActions.Add(() =>
		{
			Player.TrySetVelocityZero();
			Player.Crouch();
		});

		exitActions.Add(() =>
		{
			Player.Stand();
		});
	}
}
