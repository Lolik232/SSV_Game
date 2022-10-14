using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLedgeHoldState", menuName = "Player/States/Ledge Hold")]

public class PlayerLedgeHoldStateSO : PlayerStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool LedgeClimbCondition() => inputReader.moveInput.x == player.facingDirection ||
																	inputReader.moveInput.y == 1;

		bool InAirCondition() => inputReader.moveInput.x == -player.facingDirection ||
														 inputReader.moveInput.y == -1;

		transitions.Add(new TransitionItem(states.ledgeClimb, LedgeClimbCondition));
		transitions.Add(new TransitionItem(states.inAir, InAirCondition));

		enterActions.Add(() =>
		{
			abilities.wallJump.RestoreAmountOfUsages();
			player.HoldPosition(player.ledgeStartPosition);
		});

		updateActions.Add(() =>
		{
			player.HoldPosition(player.ledgeStartPosition);
		});
	}
}
