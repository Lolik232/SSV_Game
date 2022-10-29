using System;

using UnityEngine;

public class AbilitiesManagerBase : ComponentBase
{
	[HideInInspector] [NonSerialized] protected new AbilitiesManagerSO component;

	protected override void Awake()
	{
		component = (AbilitiesManagerSO)base.component;

		base.Awake();
	}
}
