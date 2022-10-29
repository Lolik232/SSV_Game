using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public abstract class AnimatedComponentSO : EntityComponentSO, IAnimated
{
	private int _animationIndex;

	[SerializeField] private List<AnimationBool> _animBools = new();

	protected List<UnityAction<int>> animationActions = new();
	protected List<UnityAction> animationFinishActions = new();

	protected Animator anim;

	protected override void OnEnable()
	{
		base.OnEnable();

		enterActions.Add(() =>
		{
			Utility.SetAnimBoolsOnEnter(anim, _animBools);
		});

		exitActions.Add(() =>
		{
			Utility.SetAnimBoolsOnExit(anim, _animBools);
		});

		animationActions.Clear();
		animationFinishActions.Clear();
	}

	public override void InitialzeBase(GameObject baseObject)
	{
		base.InitialzeBase(baseObject);

		anim = baseObject.GetComponent<Animator>();
	}

	public virtual void OnAnimationTrigger()
	{
		if (isActive)
		{
			Utility.ApplyActions(animationActions, _animationIndex++);
		}
	}

	public virtual void OnAnimationFinishTrigger()
	{
		if (isActive)
		{
			Utility.ApplyActions(animationFinishActions);
			_animationIndex = 0;
		}
	}
}

[Serializable]
public struct AnimationBool
{
	public string name;
	public bool onEnterValue;
	public bool onExitValue;

	public AnimationBool(string name, bool onEnterValue, bool onExitValue)
	{
		this.name = name;
		this.onEnterValue = onEnterValue;
		this.onExitValue = onExitValue;
	}
}
