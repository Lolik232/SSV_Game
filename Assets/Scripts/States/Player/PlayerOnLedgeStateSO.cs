using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerOnLedgeState", menuName = "States/On Ledge/Player")]

public class PlayerOnLedgeStateSO : StateSO
{
	[HideInInspector] protected new PlayerSO entity;

	protected override void OnEnable()
	{
		entity = (PlayerSO)base.entity;

		base.OnEnable();

		enterActions.Add(() =>
		{
			entity.abilities.attack.HoldDirection(-entity.checkers.wallDirection);
			entity.HoldPosition(entity.checkers.ledgeStartPosition);
		});

		exitActions.Add(() =>
		{
			entity.abilities.attack.ReleaseDirection();
			entity.ReleasePosition();
		});
	}

	protected virtual bool InAirCondition()
	{
		return !entity.checkers.grounded;
	}

	protected virtual void InAirAction()
	{
		if (entity.checkers.touchingCeiling && !entity.isStanding)
		{
			entity.MoveToY(entity.Position.y - (entity.StandSize.y - entity.CrouchSize.y));
		}
	}

	protected virtual bool GroundedCondition()
	{
		return !entity.checkers.grounded;
	}

	protected virtual void GroundedAirAction()
	{
		entity.states.grounded.isClimbFinish = false;
	}
}
