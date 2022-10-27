using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineBase : ComponentBase, IAnimated
{
	[HideInInspector] protected new StateMachineSO component;

	protected override void Awake()
	{
		component = base.component as StateMachineSO;

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
