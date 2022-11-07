using System;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(AbilitiesManager))]

public abstract class Ability : ComponentBase
{
	[SerializeField] private int _amountOfUsages;

	private List<dynamic> _abilityStates = new();

	protected List<Func<bool>> enterConditions = new();
	protected List<Func<bool>> exitConditions = new();

	private readonly Blocker _blocker = new();

	public dynamic Current
	{
		get;
		private set;
	}

	public dynamic Default
	{
		get;
		protected set;
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

	public int AmountOfUsages
	{
		get;
		private set;
	}

	protected bool IsContinuous
	{
		private get;
		set;
	}

	protected virtual void Awake()
	{
		SetEmpty();
	}

	public void Block()
	{
		OnExit();
		_blocker.AddBlock();
	}

	public void Unlock()
	{
		_blocker.RemoveBlock();
	}

	public override void OnEnter()
	{
		OnEnter(Default);
	}

	public void OnEnter<AbilityT>(AbilityState<AbilityT> aS) where AbilityT : Ability
	{
		if (IsActive || IsLocked || AmountOfUsages <= 0 || !CheckForEnter())
		{
			return;
		}

		ApplyEnterActions();
		Initialize(aS);
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

		if ((IsContinuous && AmountOfUsages <= 0) || CheckForExit())
		{
			OnExit();
			return;
		}

		ApplyUpdateActions();
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		if (!IsContinuous)
		{
			DecreaseAmountOfUsages();
		}

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

	public void Restore()
	{
		AmountOfUsages = _amountOfUsages;
	}

	public void SetEmpty()
	{
		AmountOfUsages = 0;
	}

	public void DecreaseAmountOfUsages()
	{
		if (AmountOfUsages > 0)
		{
			AmountOfUsages--;
		}
	}
}