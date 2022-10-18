using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLandState", menuName = "Player/States/Grounded/Land")]

public class PlayerLandStateSO : PlayerGroundedStateSO
{
	private bool _isLandFinished;

	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => _isLandFinished;
		bool CrouchIdleCondition() => inputReader.moveInput.y < 0;
		bool MoveCondition() => inputReader.moveInput.x != 0;

		transitions.Add(new TransitionItem(states.idle, IdleCondition));
		transitions.Add(new TransitionItem(states.crouchIdle, CrouchIdleCondition));
		transitions.Add(new TransitionItem(states.move, MoveCondition));

		enterActions.Add(() =>
		{
			player.TrySetVelocityZero();
			_isLandFinished = false;
		});

		animationFinishActions.Add(() =>
		{
			_isLandFinished = true;
		});
	}
}
