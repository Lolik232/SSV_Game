using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLedgeClimbAbility", menuName = "Abilities/Ledge Climb/Player")]

public class PlayerLedgeClimbAbilitySO : AbilitySO
{
	[HideInInspector] [NonSerialized] protected new PlayerSO entity;

	protected override void OnEnable()
	{
		entity = base.entity as PlayerSO;

		base.OnEnable();

		enterConditions.Add(() => entity.checkers.touchingWall &&
															!entity.checkers.touchingLedge &&
															(entity.controller.move.x == entity.direction.facing ||
															 entity.controller.move.y == 1));

		enterActions.Add(() =>
		{
			entity.checkers.DetermineLedgePosition();
			entity.HoldDirection(-entity.checkers.wallDirection);
			entity.HoldPosition(entity.checkers.ledgeStartPosition);
		});

		exitActions.Add(() =>
		{
			entity.ReleaseDirection();
			entity.ReleasePosition();
			entity.MoveTo(entity.checkers.ledgeEndPosition);
		});
	}
}
