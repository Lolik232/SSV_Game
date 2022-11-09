using System;
using System.Collections.Generic;

using UnityEngine;

using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(AbilitiesManager))]

public abstract class Ability : ComponentBase
{
	private List<dynamic> _abilityStates = new();

	protected List<Func<bool>> enterConditions = new();
	protected List<Func<bool>> exitConditions = new();

	private readonly Blocker _blocker = new();

	private dynamic _requested;

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

	public bool Permited
	{
		get;
		set;
	}

	protected virtual void Awake()
	{

	}

	protected virtual void Start()
	{
		CancelRequest();
	}

	public void Request<AbilityT>(AbilityState<AbilityT> aS) where AbilityT : Ability
	{
		if (aS.Ability == Default.Ability)
		{
			_requested = aS;
		}
		else
		{
			throw new ArgumentException("Ability State " + aS + " Doesn`t Relate To " + Default.Ability);
		}
	}

	public void CancelRequest()
	{
		_requested = Default;
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
		if (IsActive || IsLocked || !Permited || !CheckForEnter())
		{
			return;
		}

		ApplyEnterActions();
		Initialize(_requested);
		CancelRequest();
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

		if (IsLocked || !Permited || CheckForExit())
		{
			OnExit();
			return;
		}

		ApplyUpdateActions();
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