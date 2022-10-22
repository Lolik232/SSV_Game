using System;

using UnityEngine;

public abstract class PlayerStateSO : StateSO
{
	protected new PlayerDataSO data => (PlayerDataSO)base.data;

	protected override void OnEnable()
	{
		base.OnEnable();

		fixedUpdateActions.Add(() =>
		{
			data.checkers.DoChecks();
		});
	}
}
