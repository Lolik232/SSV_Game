using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnLedgeStateSO : PlayerStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		enterActions.Add(() =>
		{
			data.abilities.attack.HoldDirection(data.checkers.wallDirection);
		});

		exitActions.Add(() =>
		{
			data.abilities.attack.ReleaseDirection();
		});
	}
}
