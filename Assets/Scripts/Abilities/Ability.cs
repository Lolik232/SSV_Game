using System;
using System.Collections.Generic;

using UnityEngine;

public abstract class Ability : MonoBehaviour, IAbility, IActivated, IBlockable
{
	[SerializeField] private AbilityParameter _cooldown;
	[SerializeField] private AbilityParameter _duration;
	[SerializeField] private AbilityParameterInt _amountOfUsages;
	[Space]
	[SerializeField] private List<AnimationBool> _animBools;
	[Space]
	[SerializeField] private List<BlockedState> _blockedStates;
	[Space]
	[SerializeField] private List<Condition> _enterConditions;
	[SerializeField] private List<Condition> _exitConditions;
	[Space]
	[SerializeField] private List<PermitedAbility> _blockedAbilities;

	private readonly Blocker _blocker = new();

	private Animator _anim;

	private bool _isActive;
	private float _startTime;
	private float _endTime;

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
	public float Cooldown
	{
		get => _cooldown.value.Current;
		set => _cooldown.value.Set(value);
	}
	public float Duration
	{
		get => _duration.value.Current;
		set => _duration.value.Set(value);
	}
	public int AmountOfUsages
	{
		get => _amountOfUsages.value.Current;
		set => _duration.value.Set(value);
	}

	protected virtual void Awake()
	{
		_anim = GetComponent<Animator>();
		_cooldown.value.Initialize();
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

	public void OnEnter()
	{
		if (IsActive)
		{
			return;
		}

		ApplyPrepareActions();

		if (_amountOfUsages.required && AmountOfUsages == 0 || InactiveTime < Cooldown)
		{
			return;
		}

		if (!DoEnterChecks())
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

	public void OnUpdate()
	{
		if (!IsActive)
		{
			return;
		}

		if (DoExitChecks())
		{
			OnExit();
			return;
		}

		ApplyUpdateActions();
	}

	protected virtual void ApplyEnterActions()
	{
		IsActive = true;
		AmountOfUsages--;
		Utility.BlockAll(_blockedAbilities);
		Utility.BlockAll(_blockedStates);
		Utility.SetAnimBoolsOnEnter(_anim, _animBools);
		_startTime = Time.time;
	}

	protected virtual void ApplyExitActions()
	{
		IsActive = false;
		Utility.UnlockAll(_blockedAbilities);
		Utility.UnlockAll(_blockedStates);
		_endTime = Time.time;
	}

	protected virtual void ApplyUpdateActions()
	{
	
	}

	protected virtual void ApplyPrepareActions()
	{
		
	}

	private bool DoEnterChecks()
	{
		foreach (var condition in _enterConditions)
		{
			if (!condition.DoChecks())
			{
				return false;
			}
		}

		return true;
	}

	private bool DoExitChecks()
	{
		foreach (var condition in _exitConditions)
		{
			if (condition.DoChecks())
			{
				return true;
			}
		}

		return false;
	}
}

[Serializable]
public struct AbilityParameter
{
	public Parameter value;
	public bool required;

	public AbilityParameter(Parameter value, bool required)
	{
		this.value = value;
		this.required = required;
	}
}

[Serializable]
public struct AbilityParameterInt
{
	public ParameterInt value;
	public bool required;

	public AbilityParameterInt(ParameterInt value, bool required)
	{
		this.value = value;
		this.required = required;
	}
}

[Serializable]
public struct PermitedAbility
{
	[SerializeField] private string _description;
	public Ability component;
	public bool needHardExit;
}