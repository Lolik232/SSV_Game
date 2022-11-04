using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(StateMachine))]

public abstract class State : MonoBehaviour, IState, IActivated, IBlockable
{
	[SerializeField] private List<AnimationBool> _animBools;
	[Space]
	[SerializeField] private List<TransitionItem> _transitions;
	[Space]
	[SerializeField] private List<BlockedAbility> _permitedAbilities;

	private readonly Blocker _blocker = new();

	private Animator _anim;

	private State _defaultTransition;

	private bool _isActive;
	private float _startTime;
	private float _endTime;

	public State Default
	{
		get => _defaultTransition;
	}
	public List<TransitionItem> Transitions
	{
		get => _transitions;
	}
	public bool IsLocked
	{
		get => _blocker.IsLocked;
	}
	public bool IsActive
	{
		get => _isActive;
		private set => _isActive = value;
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

	public void SetBlockedTransition(State blockedTransition)
	{
		_defaultTransition = blockedTransition;
	}

	public void Block()
	{
		_blocker.AddBlock();
	}

	public void Unlock()
	{
		_defaultTransition = null;
		_blocker.RemoveBlock();
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
public struct BlockedState
{
	[SerializeField] private string _description;
	public State component;
	public State target;
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
public class TransitionItem : IComponent, ICondition
{
	[SerializeField] private string _description;
	public State target;
	public List<Condition> conditions;

	public bool DoChecks()
	{
		if (target.IsLocked)
		{
			return false;
		}

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