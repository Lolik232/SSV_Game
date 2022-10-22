using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLedgeHoldState", menuName = "Player/States/Ledge Hold")]

public class PlayerLedgeHoldStateSO : PlayerOnLedgeStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool LedgeClimbCondition() => data.input.moveInput.x == player.facingDirection ||
																	data.input.moveInput.y == 1;

		bool InAirCondition() => data.input.moveInput.x == -player.facingDirection ||
														 data.input.moveInput.y == -1;

		transitions.Add(new TransitionItem(data.states.ledgeClimb, LedgeClimbCondition));
		transitions.Add(new TransitionItem(data.states.inAir, InAirCondition));

		enterActions.Add(() =>
		{
			data.abilities.wallJump.RestoreAmountOfUsages();
		});

		exitActions.Add(() =>
		{
			player.ReleasePosition();
		});
	}
}
