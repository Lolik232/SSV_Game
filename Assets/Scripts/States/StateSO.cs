using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public abstract class StateSO : AnimatedComponentSO, IBlockable
{
	[SerializeField] private List<BlockedAbility> _blockedAbilities = new();

	private StateSO _blockedTransition;

	protected List<TransitionItem> transitions = new();

	private readonly Blocker _blocker = new();

	protected override void OnEnable()
	{
		base.OnEnable();

		enterActions.Add(() =>
		{
			Utility.BlockAll(_blockedAbilities);
			Utility.SetAnimBools(anim, animBoolNames, true);
		});

		exitActions.Add(() =>
		{
			Utility.UnlockAll(_blockedAbilities);
			Utility.SetAnimBools(anim, animBoolNames, false);
		});

		fixedUpdateActions.Add(() =>
		{
			entity.checkers.DoChecks();
		});
	}

	public void Initialize(Animator animator) => anim = animator;

	public override void OnUpdate()
	{
		if (!CheckIfNeedTransition())
		{
			base.OnUpdate();
		}
	}

	private bool CheckIfNeedTransition()
	{
		if (!isActive)
		{
			return false;
		}

		for (int i = 0; i < transitions.Count; i++)
		{
			if (!transitions[i].toState._blocker.IsLocked && transitions[i].condition())
			{
				transitions[i].action();
				entity.states.GetNext(transitions[i].toState);
				return true;
			}
		}

		return false;
	}

	public void SetBlockedTransition(StateSO blockedTransition)
	{
		_blockedTransition = blockedTransition;
	}

	public void Block(bool needHardExit)
	{
		if (needHardExit && isActive)
		{
			entity.states.GetNext(_blockedTransition);
		}

		_blocker.AddBlock();
	}

	public void Unlock()
	{
		_blockedTransition = null;
		_blocker.RemoveBlock();
	}
}

public struct TransitionItem
{
	public StateSO toState;
	public Func<bool> condition;
	public UnityAction action;

	public TransitionItem(StateSO toState, Func<bool> condition, UnityAction action)
	{
		this.toState = toState;
		this.condition = condition;
		this.action = action;
	}
}

[Serializable]
public struct BlockedState
{
	public StateSO component;
	public StateSO target;
	public bool needHardExit;

	public BlockedState(StateSO component, StateSO target, bool needHardExit)
	{
		this.component = component;
		this.target = target;
		this.needHardExit = needHardExit;
	}
}
