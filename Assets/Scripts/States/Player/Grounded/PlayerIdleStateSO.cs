using UnityEngine;

[CreateAssetMenu(fileName = "PlayerIdleState", menuName = "Player/States/Grounded/Idle")]
public class PlayerIdleStateSO : GroundedStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool CrouchIdleCondition() => entity.controller.move.y < 0;

		bool MoveCondition() => entity.controller.move.x != 0;

		transitions.Add(new TransitionItem(entity.states.crouchIdle, CrouchIdleCondition));
		transitions.Add(new TransitionItem(entity.states.move, MoveCondition));

		enterActions.Add(() =>
		{
			entity.TrySetVelocityZero();
		});
	}
}
