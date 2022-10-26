using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTaskManagerBase : ComponentBase
{
	[HideInInspector] protected new MultiTaskManagerSO component;

	protected override void Awake()
	{
		component = (MultiTaskManagerSO)base.component;

		base.Awake();
	}
}
