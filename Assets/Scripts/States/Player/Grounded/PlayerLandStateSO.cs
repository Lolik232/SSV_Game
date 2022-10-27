using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLandState", menuName = "Player/States/Grounded/Land")]

public class PlayerLandStateSO : GroundedStateSO
{
	private bool _isLandFinished;

	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => _isLandFinished;
		bool CrouchIdleCondition() => entity.controller.move.y < 0;
		bool MoveCondition() => entity.controller.move.x != 0;

		transitions.Add(new TransitionItem(entity.states.idle, IdleCondition));
		transitions.Add(new TransitionItem(entity.states.crouchIdle, CrouchIdleCondition));
		transitions.Add(new TransitionItem(entity.states.move, MoveCondition));

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
