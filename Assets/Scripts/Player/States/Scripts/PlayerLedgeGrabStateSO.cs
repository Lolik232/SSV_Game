using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLedgeGrabState", menuName = "Player/States/Ledge Grab")]

public class PlayerLedgeGrabStateSO : PlayerStateSO
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
			player.HoldPosition(player.ledgeStartPosition);
		});

		updateActions.Add(() =>
		{
			player.HoldPosition(player.ledgeStartPosition);
		});

		animationFinishActions.Add(() =>
		{
			_grabFinish = true;
		});
	}
}
