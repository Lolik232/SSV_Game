using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AnimatedComponentSO : EntityComponentSO, IAnimated
{
	private int _animationIndex;

	[SerializeField] protected List<string> animBoolNames = new();
	protected List<UnityAction<int>> animationActions = new();
	protected List<UnityAction> animationFinishActions = new();

	protected Animator anim;

	protected override void OnEnable()
	{
		base.OnEnable();

		enterActions.Add(() =>
		{
			Utility.SetAnimBools(anim, animBoolNames, true);
		});

		exitActions.Add(() =>
		{
			Utility.SetAnimBools(anim, animBoolNames, false);
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
