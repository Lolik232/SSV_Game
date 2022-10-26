using System;

using UnityEngine;

public abstract class PlayerStateSO : StateSO
{
	[HideInInspector] protected new PlayerDataSO data; 

	protected override void OnEnable()
	{
		data = (PlayerDataSO)base.data;

		base.OnEnable();
	}
}
