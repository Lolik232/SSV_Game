using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class AnimatedComponentBase : ComponentBase, IAnimated
{
	[HideInInspector] protected new AnimatedComponentSO component;

	protected override void Awake()
	{
		component = base.component as AnimatedComponentSO;

		base.Awake();
	}

	public void OnAnimationTrigger() => component.OnAnimationTrigger();

	public void OnAnimationFinishTrigger() => component.OnAnimationFinishTrigger();
}
