using UnityEngine;

[CreateAssetMenu(fileName = "PlayerIdleState", menuName = "Player/States/Grounded/Idle")]
public class PlayerIdleStateSO : PlayerGroundedStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool CrouchIdleCondition() => inputReader.moveInput.y < 0;

		bool MoveCondition() => inputReader.moveInput.x != 0;

		transitions.Add(new TransitionItem(states.crouchIdle, CrouchIdleCondition));
		transitions.Add(new TransitionItem(states.move, MoveCondition));

		enterActions.Add(() =>
		{
			player.TrySetVelocityZero();
		});
	}
}
