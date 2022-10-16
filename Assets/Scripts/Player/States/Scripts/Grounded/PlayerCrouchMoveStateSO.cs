using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCrouchMoveState", menuName = "Player/States/Grounded/Crouch Move")]

public class PlayerCrouchMoveStateSO : PlayerGroundedStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool MoveCondition() => inputReader.moveInput.y > -1 &&
														!player.isTouchingCeiling;

		bool CrouchIdleCondition() => inputReader.moveInput.x == 0;

		transitions.Add(new TransitionItem(states.move, MoveCondition));
		transitions.Add(new TransitionItem(states.crouchIdle, CrouchIdleCondition));

		enterActions.Add(() =>
		{
			player.Crouch();
		});

		updateActions.Add(() =>
		{
			player.CheckIfShouldFlip(inputReader.moveInput.x);
			player.TrySetVelocityX(inputReader.moveInput.x * parameters.crouchMoveSpeed);
		});

		exitActions.Add(() =>
		{
			player.Stand();
		});
	}
}
