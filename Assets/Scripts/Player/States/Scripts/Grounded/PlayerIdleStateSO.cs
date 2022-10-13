using UnityEngine;

[CreateAssetMenu(fileName = "PlayerIdleState", menuName = "Player/States/Grounded/Idle")]
public class PlayerIdleStateSO : PlayerGroundedStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool CrouchIdleCondition() => Player.moveInput.y < 0;

		bool MoveCondition() => Player.moveInput.x != 0;

		transitions.Add(new TransitionItem(states.crouchIdle, CrouchIdleCondition));
		transitions.Add(new TransitionItem(states.move, MoveCondition));

		enterActions.Add(() =>
		{
			Player.TrySetVelocityZero();
		});
	}
}
