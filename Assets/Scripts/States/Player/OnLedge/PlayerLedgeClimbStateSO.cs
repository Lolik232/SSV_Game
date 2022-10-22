using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLedgeClimbState", menuName = "Player/States/Ledge Climb")]

public class PlayerLedgeClimbStateSO : PlayerOnLedgeStateSO
{
	private bool _climbFinished;

	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => _climbFinished &&
														!data.checkers.isTouchingCeilingWhenClimb;

		bool CrouchIdleCondition() => _climbFinished &&
																	data.checkers.isTouchingCeilingWhenClimb;

		transitions.Add(new TransitionItem(data.states.idle, IdleCondition));
		transitions.Add(new TransitionItem(data.states.crouchIdle, CrouchIdleCondition));

		enterActions.Add(() =>
		{
			_climbFinished = false;
			player.HoldPosition(data.checkers.ledgeStartPosition);
		});

		exitActions.Add(() =>
		{
			player.transform.position = data.checkers.ledgeEndPosition;
			player.ReleasePosition();
		});

		animationFinishActions.Add(() =>
		{
			_climbFinished = true;
		});
	}
}
