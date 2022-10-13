using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCrouchMoveState", menuName = "Player/States/Grounded/Crouch Move")]

public class PlayerCrouchMoveStateSO : PlayerGroundedStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool MoveCondition() => Player.moveInput.y > -1 &&
														!Player.isTouchingCeiling;

		bool CrouchIdleCondition() => Player.moveInput.x == 0;

		transitions.Add(new TransitionItem(states.move, MoveCondition));
		transitions.Add(new TransitionItem(states.crouchIdle, CrouchIdleCondition));

		enterActions.Add(() =>
		{
			Player.Crouch();
		});

		updateActions.Add(() =>
		{
			Player.CheckIfShouldFlip(Player.moveInput.x);
			Player.TrySetVelocityX(Player.moveInput.x * Player.CrouchMoveSpeed);
		});

		exitActions.Add(() =>
		{
			Player.Stand();
		});
	}
}
