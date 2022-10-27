using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManagerBase : ComponentBase
{
	[HideInInspector] protected new AbilitiesManagerSO component;

	protected override void Awake()
	{
		component = (AbilitiesManagerSO)base.component;

		base.Awake();
	}
}
