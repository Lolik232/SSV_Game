using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
