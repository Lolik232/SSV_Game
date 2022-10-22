using UnityEngine;

[CreateAssetMenu(fileName = "PlayerIdleState", menuName = "Player/States/Grounded/Idle")]
public class PlayerIdleStateSO : PlayerGroundedStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool CrouchIdleCondition() => data.input.moveInput.y < 0;

		bool MoveCondition() => data.input.moveInput.x != 0;

		transitions.Add(new TransitionItem(data.states.crouchIdle, CrouchIdleCondition));
		transitions.Add(new TransitionItem(data.states.move, MoveCondition));

		enterActions.Add(() =>
		{
			entity.TrySetVelocityZero();
		});
	}
}
