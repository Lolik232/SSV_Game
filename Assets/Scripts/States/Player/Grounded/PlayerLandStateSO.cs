using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLandState", menuName = "Player/States/Grounded/Land")]

public class PlayerLandStateSO : PlayerGroundedStateSO
{
	private bool _isLandFinished;

	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => _isLandFinished;
		bool CrouchIdleCondition() => data.controller.move.y < 0;
		bool MoveCondition() => data.controller.move.x != 0;

		transitions.Add(new TransitionItem(data.states.idle, IdleCondition));
		transitions.Add(new TransitionItem(data.states.crouchIdle, CrouchIdleCondition));
		transitions.Add(new TransitionItem(data.states.move, MoveCondition));

		enterActions.Add(() =>
		{
			entity.TrySetVelocityZero();
			_isLandFinished = false;
		});

		animationFinishActions.Add(() =>
		{
			_isLandFinished = true;
		});
	}
}
