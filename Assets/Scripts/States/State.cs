using System;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(StateMachine))]

public abstract class State : MonoBehaviour
{
	[SerializeField] private List<AnimationBool> _animBools;

	protected List<StateTransitionItem> transitions = new();

	private List<IChecker> _checkers = new();

	private Animator _anim;
	private StateMachine _stateMachine;

	private float _startTime;
	private float _endTime;

	public List<StateTransitionItem> Transitions
	{
		get => transitions;
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

	protected virtual void Awake()
	{
		_anim = GetComponent<Animator>();
		_stateMachine = GetComponent<StateMachine>();
		GetComponents(_checkers);
	}

	private void TryGetTransition()
	{
		foreach (var transition in Transitions)
		{
			if (transition.condition())
			{
				_stateMachine.GetTransition(transition.target);
				transition.action?.Invoke();
				return;
			}
		}
	}

	public void OnEnter()
	{
		if (IsActive)
		{
			return;
		}

		ApplyEnterActions();

		foreach (var checker in _checkers)
		{
			checker.UpdateCheckersPosition();
			checker.DoChecks();
		}
	}

	public void OnExit()
	{
		if (!IsActive)
		{
			return;
		}

		ApplyExitActions();
	}

	public void OnUpdate()
	{
		if (!IsActive)
		{
			return;
		}

		TryGetTransition();

		if (!IsActive)
		{
			return;
		}

		ApplyUpdateActions();
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

	protected virtual void ApplyUpdateActions()
	{

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

public struct StateTransitionItem
{
	public State target;
	public Func<bool> condition;
	public Action action;

	public StateTransitionItem(State target, Func<bool> condition, Action action = null)
	{
		this.target = target;
		this.condition = condition;
		this.action = action;
	}
}