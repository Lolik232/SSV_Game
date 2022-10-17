using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public abstract class StateSO : ScriptableObject
{
	private bool _isActive;
	private int _animIndex;

	protected float startTime;

	[SerializeField] private List<string> _animBoolNames = new();
	[SerializeField] private List<string> _animTriggerNames = new();
	[SerializeField] private List<AbilitySO> _blockedAbilities = new();
	[SerializeField] private StateSO _defaultState;

	protected List<TransitionItem> transitions = new();
	protected List<UnityAction> enterActions = new();
	protected List<UnityAction> exitActions = new();
	protected List<UnityAction> updateActions = new();
	protected List<UnityAction> checks = new();
	protected List<UnityAction> animationFinishActions = new();
	protected List<UnityAction<int>> animationActions = new();

	protected Animator anim;

	private StateMachine _machine;

	public Blocker blocker = new();

	protected virtual void OnEnable()
	{
		_isActive = false;

		_animIndex = 0;
		startTime = 0f;

		transitions.Clear();
		updateActions.Clear();
		animationFinishActions.Clear();
		animationActions.Clear();
		checks.Clear();

		enterActions = new List<UnityAction> { () =>
			{
				_isActive = true;
				startTime = Time.time;
				foreach (var ability in _blockedAbilities)
				{
						ability.Terminate();
						ability.blocker.AddBlock();
				}

				foreach (var name in _animBoolNames)
				{
						anim.SetBool(name, true);
				}

				foreach (var name in _animTriggerNames)
				{
						anim.SetTrigger(name);
				}
			}};
		exitActions = new List<UnityAction> { () =>
			{
				_isActive = false;
				foreach (var ability in _blockedAbilities)
				{
						ability.blocker.RemoveBlock();
				}

				foreach (var name in _animBoolNames)
				{
						anim.SetBool(name, false);
				}
			}};
	}

	protected void InitializeAnimator(Animator animator) => anim = animator;
	protected void InitializeMachine(StateMachine stateMachine) => _machine = stateMachine;

	public void OnStateEnter()
	{
		if (_isActive)
		{
			return;
		}

		foreach (var action in enterActions)
		{
			action();
		}

		foreach (var check in checks)
		{
			check();
		}
	}

	public void OnStateExit()
	{
		if (!_isActive)
		{
			return;
		}

		foreach (var action in exitActions)
		{
			action();
		}
	}

	public void OnStateUpdate()
	{
		if (_defaultState != null && blocker.IsLocked)
		{
			_machine.GetTransitionState(_defaultState);
			return;
		}

		foreach (var transition in transitions)
		{
			if (!_isActive)
			{
				return;
			}

			if (!transition.toState.blocker.IsLocked && transition.condition())
			{
				transition.action?.Invoke();
				_machine.GetTransitionState(transition.toState);
				return;
			}
		}

		foreach (var action in updateActions)
		{
			if (!_isActive)
			{
				return;
			}

			action();
		}
	}

	public void OnFixedUpdate()
	{
		foreach (var check in checks)
		{
			if (!_isActive)
			{
				return;
			}

			check();
		}
	}

	public void OnAnimationFinishTrigger()
	{
		foreach (var action in animationFinishActions)
		{
			if (!_isActive)
			{
				return;
			}

			action();
		}

		_animIndex = 0;
	}

	public void OnAnimationTrigger()
	{
		foreach (var action in animationActions)
		{
			if (!_isActive)
			{
				return;
			}

			action(_animIndex);
		}

		_animIndex++;
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