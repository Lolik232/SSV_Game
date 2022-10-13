using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLandState", menuName = "Player/States/Grounded/Land")]

public class PlayerLandStateSO : PlayerGroundedStateSO
{
	private bool _isLandFinished;

	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => _isLandFinished;
		bool CrouchIdleCondition() => Player.moveInput.y < 0;
		bool MoveCondition() => Player.moveInput.x != 0;

		transitions.Add(new TransitionItem(states.idle, IdleCondition));
		transitions.Add(new TransitionItem(states.crouchIdle, CrouchIdleCondition));
		transitions.Add(new TransitionItem(states.move, MoveCondition));

		enterActions.Add(() =>
		{
			Player.TrySetVelocityZero();
			_isLandFinished = false;
		});

		animationFinishActions.Add(() =>
		{
			_isLandFinished = true;
		});
	}
}
