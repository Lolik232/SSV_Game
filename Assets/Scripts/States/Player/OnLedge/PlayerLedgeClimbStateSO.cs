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
														!entity.checkers.touchingCeilingWhenClimb;

		bool CrouchIdleCondition() => _climbFinished &&
																	entity.checkers.touchingCeilingWhenClimb;

		transitions.Add(new TransitionItem(entity.states.idle, IdleCondition));
		transitions.Add(new TransitionItem(entity.states.crouchIdle, CrouchIdleCondition));

		enterActions.Add(() =>
		{
			_climbFinished = false;
		});

		exitActions.Add(() =>
		{
			entity.MoveTo(entity.checkers.ledgeEndPosition);
		});

		animationFinishActions.Add(() =>
		{
			_climbFinished = true;
		});
	}
}
