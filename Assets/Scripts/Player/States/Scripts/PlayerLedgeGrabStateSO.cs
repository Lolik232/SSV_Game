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
			Player.isHanging = true;
			abilities.jump.SetAmountOfUsagesToZero();
			Player.HoldPosition(Player.ledgeStartPosition);
		});

		updateActions.Add(() =>
		{
			Player.HoldPosition(Player.ledgeStartPosition);
		});

		animationFinishActions.Add(() =>
		{
			_grabFinish = true;
		});
	}
}
