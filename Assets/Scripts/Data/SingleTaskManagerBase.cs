using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTaskManagerBase : ComponentBase, IAnimated
{
	[HideInInspector] protected new SingleTaskManagerSO component;

	protected override void Awake()
	{
		component = (SingleTaskManagerSO)base.component;

		base.Awake();
	}

	public void OnAnimationFinishTrigger()
	{
		component.OnAnimationFinishTrigger();
	}

	public void OnAnimationTrigger()
	{
		component.OnAnimationTrigger();
	}
}
