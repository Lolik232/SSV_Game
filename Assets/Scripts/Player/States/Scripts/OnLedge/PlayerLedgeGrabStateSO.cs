using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLedgeGrabState", menuName = "Player/States/Ledge Grab")]

public class PlayerLedgeGrabStateSO : PlayerOnLedgeStateSO
{
	private bool _grabFinish;

	protected override void OnEnable()
	{
		base.OnEnable();

		bool LedgeHoldCondition() => _grabFinish;

		transitions.Add(new TransitionItem(states.ledgeHold, LedgeHoldCondition));

		enterActions.Add(() =>
		{
			_grabFinish = false;
			abilities.jump.SetAmountOfUsagesToZero();
			Tuple<int, int> ids =  player.HoldPosition(player.ledgeStartPosition);
			gravityId = ids.Item1;
			velocityId = ids.Item2;
		});

		animationFinishActions.Add(() =>
		{
			_grabFinish = true;
		});
	}
}
