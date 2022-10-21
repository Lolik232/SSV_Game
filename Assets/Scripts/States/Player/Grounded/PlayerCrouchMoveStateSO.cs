using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCrouchMoveState", menuName = "Player/States/Grounded/Crouch Move")]

public class PlayerCrouchMoveStateSO : PlayerGroundedStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool MoveCondition() => data.input.moveInput.y > -1 &&
														!data.checkers.isTouchingCeiling;

		bool CrouchIdleCondition() => data.input.moveInput.x == 0;

		transitions.Add(new TransitionItem(data.states.move, MoveCondition));
		transitions.Add(new TransitionItem(data.states.crouchIdle, CrouchIdleCondition));

		enterActions.Add(() =>
		{
			player.Crouch();
		});

		updateActions.Add(() =>
		{
			player.CheckIfShouldFlip(data.input.moveInput.x);
			player.TrySetVelocityX(data.input.moveInput.x * data.parameters.crouchMoveSpeed);
		});

		exitActions.Add(() =>
		{
			player.Stand();
		});
	}
}
