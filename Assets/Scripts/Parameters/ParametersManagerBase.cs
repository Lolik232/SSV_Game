using System;

using UnityEngine;

public class ParametersManagerBase : ComponentBase
{
	[HideInInspector] [NonSerialized] protected new ParametersManagerSO component;

	protected override void Awake()
	{
		component = (ParametersManagerSO)base.component;

		base.Awake();
	}
}
