using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class SingleTaskManagerBase<T> : ComponentBase, IAnimated where T : AnimatedComponentSO
{
	[HideInInspector] protected new SingleTaskManagerSO<T> component;

	protected override void Awake()
	{
		component = (SingleTaskManagerSO<T>)base.component;

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
