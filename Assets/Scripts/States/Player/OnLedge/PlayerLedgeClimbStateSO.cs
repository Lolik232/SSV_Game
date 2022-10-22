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
														!data.checkers.touchingCeilingWhenClimb;

		bool CrouchIdleCondition() => _climbFinished &&
																	data.checkers.touchingCeilingWhenClimb;

		transitions.Add(new TransitionItem(data.states.idle, IdleCondition));
		transitions.Add(new TransitionItem(data.states.crouchIdle, CrouchIdleCondition));

		enterActions.Add(() =>
		{
			_climbFinished = false;
			entity.HoldPosition(data.checkers.ledgeStartPosition);
		});

		exitActions.Add(() =>
		{
			entity.MoveTo(data.checkers.ledgeEndPosition);
			entity.ReleasePosition();
		});

		animationFinishActions.Add(() =>
		{
			_climbFinished = true;
		});
	}
}
