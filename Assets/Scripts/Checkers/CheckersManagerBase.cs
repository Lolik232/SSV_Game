using System;

using UnityEngine;

public class CheckersManagerBase : ComponentBase
{
	[HideInInspector] [NonSerialized] protected new CheckersManagerSO component;

	protected override void Awake()
	{
		component = (CheckersManagerSO)base.component;

		base.Awake();
	}
}
