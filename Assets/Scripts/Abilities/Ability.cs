using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(AbilitiesManager))]

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
	[SerializeField] private List<BlockedAbility> _blockedAbilities;

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

		_cooldown.value.Initialize();
		_duration.value.Initialize();

		foreach (var condition in _enterConditions)
		{
			condition.Initialize(gameObject);
		}

		foreach (var condition in _exitConditions)
		{
			condition.Initialize(gameObject);
		}
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

	public void TryEnter()
	{
		if (IsActive || IsLocked)
		{
			return;
		}

		ApplyPrepareActions();

		if (_amountOfUsages.required && AmountOfUsages == 0 || (_cooldown.required && InactiveTime < Cooldown))
		{
			return;
		}

		if (!DoEnterChecks())
		{
			return;
		}

		ApplyEnterActions();
	}

	public void OnEnter()
	{
		if (IsActive || IsLocked)
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
		Utility.SetAnimBoolsOnExit(_anim, _animBools);
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
		if (!IsActive)
		{
			return false;
		}

		if (_duration.required && ActiveTime > Duration)
		{
			return true;
		}

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
public struct BlockedAbility
{
	[SerializeField] private string _description;
	public Ability component;
}