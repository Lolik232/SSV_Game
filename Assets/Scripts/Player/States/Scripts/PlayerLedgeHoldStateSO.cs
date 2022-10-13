using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLedgeHoldState", menuName = "Player/States/Ledge Hold")]

public class PlayerLedgeHoldStateSO : PlayerStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool LedgeClimbCondition() => Player.moveInput.x == Player.facingDirection ||
																	Player.moveInput.y == 1;

		bool InAirCondition() => abilities.wallJump.isActive ||
														 Player.moveInput.x == -Player.facingDirection ||
														 Player.moveInput.y == -1;

		transitions.Add(new TransitionItem(states.ledgeClimb, LedgeClimbCondition));
		transitions.Add(new TransitionItem(states.inAir, InAirCondition));

		enterActions.Add(() =>
		{
			abilities.wallJump.RestoreAmountOfUsages();
			Player.HoldPosition(Player.ledgeStartPosition);
		});

		updateActions.Add(() =>
		{
			Player.HoldPosition(Player.ledgeStartPosition);
		});

		exitActions.Add(() =>
		{
			Player.isHanging = false;
		});
	}
}
