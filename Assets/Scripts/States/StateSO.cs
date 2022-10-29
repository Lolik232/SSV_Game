using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public abstract class StateSO : BaseScriptableObject, IComponent, IBlockable
{
	[SerializeField] private StateMachineSO _stateMachine;
	[SerializeField] private List<AnimationBool> _animBools;
	[Space]
	[SerializeField] private List<PermitedAbility> _permitedAbilities;
	[Space]

	protected List<TransitionItem> transitions = new();

	private StateSO _blockedTransition;

	private readonly Blocker _blocker = new();

	protected Animator anim;

	public bool IsLocked
	{
		get => _blocker.IsLocked;
	}

	protected override void OnEnable()
	{
		base.OnEnable();

		enterActions.Add(() =>
		{
			Utility.UnlockAll(_permitedAbilities);
			Utility.SetAnimBoolsOnEnter(anim, _animBools);
		});

		exitActions.Add(() =>
		{
			Utility.BlockAll(_permitedAbilities);
			Utility.SetAnimBoolsOnExit(anim, _animBools);
		});
	}

	public virtual void Initialize(GameObject origin)
	{
		anim = origin.GetComponent<Animator>();
	}

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

		foreach (var transition in transitions)
		{
			if (!transition.toState.IsLocked && transition.condition())
			{
				transition.action();
				_stateMachine.GetNext(transition.toState);
				return true;
			}
		}

		return false;
	}

	public void SetBlockedTransition(StateSO blockedTransition)
	{
		_blockedTransition = blockedTransition;
	}

	public void Block()
	{
		_stateMachine.GetNext(_blockedTransition);
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

	public BlockedState(StateSO component, StateSO target)
	{
		this.component = component;
		this.target = target;
	}
}

[Serializable]
public struct AnimationBool
{
	public enum EnableMode
	{
		Enable, 
		Disable, 
		EnableOnEnterDisableOnExit,
		DisableOnEnterEnableOnExit
	} 

	public string name;
	public EnableMode mode;

	public AnimationBool(string name, EnableMode enable)
	{
		this.name = name;
		this.mode = enable;
	}
}
