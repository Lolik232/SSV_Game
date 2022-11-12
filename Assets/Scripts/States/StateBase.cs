using System;
using System.Collections.Generic;

using UnityEngine;

public abstract class StateBase : ComponentBase
{
	[SerializeField] private string _name;

	protected List<IChecker> Checkers
	{
		get;
		private set;
	} = new();

	protected Animator Anim
	{
		get;
		private set;
	}
	public string Name
	{
		get => _name;
	}

	protected virtual void Awake()
	{
		Anim = GetComponent<Animator>();
	}

	public override void OnEnter()
	{
		if (IsActive)
		{
			return;
		}

		ApplyEnterActions();

		foreach (var checker in Checkers)
		{
			checker.UpdateCheckersPosition();
			checker.DoChecks();
		}
	}

	public override void OnExit()
	{
		if (!IsActive)
		{
			return;
		}

		ApplyExitActions();
	}

	public override void OnUpdate()
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

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		if (Name != string.Empty)
		{
			Anim.SetBool(Name, true);
			Debug.Log(Name);
		}
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		if (Name != string.Empty)
		{
			Anim.SetBool(Name, false);
		}
	}

	protected abstract void TryGetTransition();

	protected void SetAnimationSpeed(float duration)
	{
		Utility.SetAnimationSpeed(Anim, Name, duration);
	}
}

public struct TransitionItem<T> where T : StateBase
{
	public T target;
	public Func<bool> condition;
	public Action action;

	public TransitionItem(T target, Func<bool> condition, Action action = null)
	{
		this.target = target;
		this.condition = condition;
		this.action = action;
	}
}