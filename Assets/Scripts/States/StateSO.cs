using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public abstract class StateSO : AnimatedComponentSO, IBlockable
{
	[SerializeField] private List<BlockedAbility> _blockedAbilities = new();

	[SerializeField] private StateMachineSO _machine;

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
			data.checkers.DoChecks();
		});
	}

	public void Initialize(Animator animator) => anim = animator;

	public override void OnUpdate()
	{
		CheckIfNeedTransition();
		base.OnUpdate();
	}

	private void CheckIfNeedTransition()
	{
		if (!isActive)
		{
			return;
		}

		foreach (var transition in transitions)
		{
			if (!transition.toState._blocker.IsLocked && transition.condition())
			{
				transition.action?.Invoke();
				_machine.GetTransitionState(transition.toState);
				return;
			}
		}
	}

	public void SetBlockedTransition(StateSO blockedTransition)
	{
		_blockedTransition = blockedTransition;
	}

	public void Block(bool needHardExit)
	{
		if (needHardExit && isActive)
		{
			_machine.GetTransitionState(_blockedTransition);
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
	public TransitionItem(StateSO toState, Func<bool> condition, UnityAction action = null)
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

	public BlockedState(StateSO blockedState, StateSO transitionState, bool needHardExit)
	{
		this.component = blockedState;
		this.target = transitionState;
		this.needHardExit = needHardExit;
	}
}
