using System;

using UnityEngine;

public abstract class PlayerAbilitySO : AbilitySO
{
	[HideInInspector] protected new PlayerDataSO data;

	protected override void OnEnable()
	{
		data = (PlayerDataSO)base.data;
		base.OnEnable();
	}
}
