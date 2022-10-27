using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametersManagerBase : ComponentBase
{
	[HideInInspector] protected new ParametersManagerSO component;

	protected override void Awake()
	{
		component = (ParametersManagerSO)base.component;

		base.Awake();
	}
}
