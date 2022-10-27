using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLedgeHoldState", menuName = "Player/States/Ledge Hold")]

public class PlayerLedgeHoldStateSO : PlayerOnLedgeStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool LedgeClimbCondition() => entity.controller.move.x == entity.facingDirection ||
																	entity.controller.move.y == 1;

		bool InAirCondition() => entity.controller.move.x == -entity.facingDirection ||
														 entity.controller.move.y == -1;

		transitions.Add(new TransitionItem(entity.states.ledgeClimb, LedgeClimbCondition));
		transitions.Add(new TransitionItem(entity.states.inAir, InAirCondition));

		enterActions.Add(() =>
		{
			entity.abilities.wallJump.RestoreAmountOfUsages();
		});
	}
}
