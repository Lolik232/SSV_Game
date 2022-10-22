using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public abstract class StateSO : BaseScriptableObject
{
	private bool _isActive;
	private int _animIndex;

	protected float startTime;

	[SerializeField] private List<string> _animBoolNames = new();
	[SerializeField] private List<BlockedAbility> _blockedAbilities = new();

	[SerializeField] protected DataSO data;
	[SerializeField] protected EntitySO entity;
	[SerializeField] private StateMachineSO _machine;

	private StateSO _transitionStateWhenBlocked;

	protected List<TransitionItem> transitions = new();
	protected List<UnityAction> animationFinishActions = new();
	protected List<UnityAction<int>> animationActions = new();

	protected Animator anim;

	public readonly Blocker blocker = new();

	protected override void OnEnable()
	{
		_isActive = false;
		_animIndex = 0;
		startTime = 0f;

		base.OnEnable();

		enterActions = new List<UnityAction> { () =>
			{
				_isActive = true;
				startTime = Time.time;
				foreach (var ability in _blockedAbilities)
				{
					if (ability.isHardBlocked)
					{
						ability.blockedAbility.Terminate();
					}

					ability.blockedAbility.blocker.AddBlock();
				}

				foreach (var name in _animBoolNames)
				{
						anim.SetBool(name, true);
				}
			}};
		exitActions = new List<UnityAction> { () =>
			{
				_isActive = false;
				foreach (var ability in _blockedAbilities)
				{
					ability.blockedAbility.blocker.RemoveBlock();
				}

				foreach (var name in _animBoolNames)
				{
						anim.SetBool(name, false);
				}
			}};
	}

	public void Initialize(Animator animator) => anim = animator;

	public void OnStateEnter()
	{
		if (_isActive)
		{
			return;
		}

		ApplyActions(enterActions);
		ApplyActions(fixedUpdateActions);
	}

	public void OnStateExit()
	{
		if (!_isActive)
		{
			return;
		}

		ApplyActions(exitActions);
	}

	public void OnStateUpdate()
	{
		if (!_isActive)
		{
			return;
		}

		if (_transitionStateWhenBlocked != null && blocker.IsLocked)
		{
			_machine.GetTransitionState(_transitionStateWhenBlocked);
			return;
		}

		foreach (var transition in transitions)
		{
			if (!transition.toState.blocker.IsLocked && transition.condition())
			{
				transition.action?.Invoke();
				_machine.GetTransitionState(transition.toState);
				return;
			}
		}

		ApplyActions(updateActions);
	}

	public void OnStateFixedUpdate()
	{
		if (!_isActive)
		{
			return;
		}

		ApplyActions(fixedUpdateActions);
	}

	public void OnStateAnimationFinishTrigger()
	{
		if (!_isActive)
		{
			return;
		}

		ApplyActions(animationFinishActions);

		_animIndex = 0;
	}

	public void OnStateAnimationTrigger()
	{
		if (!_isActive)
		{
			return;
		}

		ApplyActions(animationActions, _animIndex);

		_animIndex++;
	}

	public void SetBlockTransition(StateSO state)
	{
		_transitionStateWhenBlocked = state;
	}

	public void ResetBlockTransition()
	{
		_transitionStateWhenBlocked = null;
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
	public StateSO blockedState;
	public StateSO transitionState;

	public BlockedState(StateSO blockedState, StateSO transitionState)
	{
		this.blockedState = blockedState;
		this.transitionState = transitionState;
	}
}
