using System;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(StateMachine))]

public abstract class State : MonoBehaviour, IState, IActivated
{
	[SerializeField] private List<AnimationBool> _animBools;
	[Space]
	[SerializeField] private List<StateTransitionItem> _transitions;
	[Space]
	[SerializeField] private List<BlockedAbility> _permitedAbilities;

	private Animator _anim;

	private float _startTime;
	private float _endTime;

	public List<StateTransitionItem> Transitions
	{
		get => _transitions;
	}
	public bool IsActive
	{
		get;
		private set;
	}
	public float ActiveTime
	{
		get => Time.time - _startTime;
	}
	public float InactiveTime
	{
		get => Time.time - _endTime;
	}
	public List<BlockedAbility> PermitedAbilities
	{
		get => _permitedAbilities;
	}

	protected virtual void Awake()
	{
		_anim = GetComponent<Animator>();

		foreach (var transition in _transitions)
		{
			transition.Initialize(gameObject);
		}
	}

	public void OnEnter()
	{
		if (IsActive)
		{
			return;
		}

		ApplyEnterActions();
	}

	public void OnExit()
	{
		if (!IsActive)
		{
			return;
		}

		ApplyExitActions();
	}

	protected virtual void ApplyEnterActions()
	{
		IsActive = true;
		Utility.SetAnimBoolsOnEnter(_anim, _animBools);
		_startTime = Time.time;
	}

	protected virtual void ApplyExitActions()
	{
		IsActive = false;
		Utility.SetAnimBoolsOnExit(_anim, _animBools);
		_endTime = Time.time;
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

[Serializable]
public class StateTransitionItem : TransitionItem<State>
{
}

[Serializable]
public class TransitionItem<T> : IComponent, ICondition
{
	[SerializeField] private string _description;
	public T target;
	public List<Condition> conditions;

	public bool DoChecks()
	{
		foreach (var condition in conditions)
		{
			if (condition.DoChecks())
			{
				return true;
			}
		}

		return false;
	}

	public void Initialize(GameObject origin)
	{
		foreach (var condition in conditions)
		{
			condition.Initialize(origin);
		}
	}
}