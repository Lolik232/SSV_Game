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

		transitions.Add(new TransitionItem(data.states.ledgeHold, LedgeHoldCondition));

		enterActions.Add(() =>
		{
			_grabFinish = false;
			data.abilities.jump.SetAmountOfUsagesToZero();
		});

		animationFinishActions.Add(() =>
		{
			_grabFinish = true;
		});
	}
}
