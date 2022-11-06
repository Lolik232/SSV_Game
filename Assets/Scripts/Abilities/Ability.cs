using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

[RequireComponent(typeof(AbilitiesManager))]

public abstract class Ability : ComponentBase
{
	private List<dynamic> _abilityStates = new();

	protected List<Func<bool>> enterConditions = new();
	protected List<Func<bool>> exitConditions = new();

	private readonly Blocker _blocker = new();

	public dynamic Current
	{
		get;
		private set;
	}

	public bool IsLocked
	{
		get => _blocker.IsLocked;
		set
		{
			if (value)
			{
				Block();
			}
			else
			{
				Unlock();
			}
		}
	}

	protected virtual void Awake()
	{
		_blocker.AddBlock();
	}

	public void Block()
	{
		_blocker.AddBlock();
	}

	public void Unlock()
	{
		_blocker.RemoveBlock();
	}

	public void TryEnter()
	{
		if (IsActive || IsLocked || !CheckForEnter())
		{
			return;
		}

		ApplyEnterActions();
	}

	public override void OnEnter()
	{
		if (IsActive)
		{
			return;
		}

		ApplyEnterActions();
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

		if (CheckForExit())
		{
			OnExit();
			return;
		}

		ApplyUpdateActions();
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		Initialize(_abilityStates.First());
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		Current.OnExit();
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		Current.OnUpdate();
	}

	private bool CheckForEnter()
	{
		foreach (var condition in enterConditions)
		{
			if (condition())
			{
				return true;
			}
		}

		return false;
	}

	private bool CheckForExit()
	{
		foreach (var condition in exitConditions)
		{
			if (condition())
			{
				return true;
			}
		}

		return false;
	}

	private void Initialize(dynamic target)
	{
		Current = target;
		Current.OnEnter();
	}

	public void GetTransition(dynamic target)
	{
		Current.OnExit();
		Current = target;
		Current.OnEnter();
	}

	protected void GetAbilityStates<AbilityT>() where AbilityT : Ability
	{
		List<AbilityState<AbilityT>> states  = new();
		GetComponents(states);
		foreach (var state in states)
		{
			_abilityStates.Add(state);
		}
	}
}