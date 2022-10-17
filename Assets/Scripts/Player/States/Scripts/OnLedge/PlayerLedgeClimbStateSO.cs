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
														!player.isTouchingCeilingWhenClimb;

		bool CrouchIdleCondition() => _climbFinished &&
																	player.isTouchingCeilingWhenClimb;

		transitions.Add(new TransitionItem(states.idle, IdleCondition));
		transitions.Add(new TransitionItem(states.crouchIdle, CrouchIdleCondition));

		enterActions.Add(() =>
		{
			_climbFinished = false;
			Tuple<int, int> ids =  player.HoldPosition(player.ledgeStartPosition);
			gravityId = ids.Item1;
			velocityId = ids.Item2;
		});

		exitActions.Add(() =>
		{
			player.transform.position = player.ledgeEndPosition;
			player.ReleasePosition(gravityId, velocityId);
		});

		animationFinishActions.Add(() =>
		{
			_climbFinished = true;
		});
	}
}
