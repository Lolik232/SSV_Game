using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLedgeClimbState", menuName = "Player/States/Ledge Climb")]

public class PlayerLedgeClimbStateSO : PlayerStateSO
{
	private bool _climbFinished;

	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => _climbFinished &&
														!player.isTouchingCeilingWhenClimb;

		bool CrouchIdleCondition() => _climbFinished &&
																	player.isTouchingCeilingWhenClimb;

		transitions.Add(new TransitionItem(states.idle, IdleCondition));
		transitions.Add(new TransitionItem(states.crouchIdle, CrouchIdleCondition));

		enterActions.Add(() =>
		{
			_climbFinished = false;
			player.HoldPosition(player.ledgeStartPosition);
		});

		updateActions.Add(() =>
		{
			player.HoldPosition(player.ledgeStartPosition);
		});

		exitActions.Add(() =>
		{
			player.transform.position = player.ledgeEndPosition;
		});

		animationFinishActions.Add(() =>
		{
			_climbFinished = true;
		});
	}
}
