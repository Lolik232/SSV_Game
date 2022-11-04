using System;
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
	[SerializeField] private List<Condition> _requiredConditions;
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
		get => _cooldown.value;
	}
	public float Duration
	{
		get => _duration.value;
	}
	public int AmountOfUsages
	{
		get => _amountOfUsages.value;
		private set => _amountOfUsages.value = value;
	}

	protected virtual void Awake()
	{
		_anim = GetComponent<Animator>();

		_blocker.AddBlock();

		foreach (var condition in _enterConditions)
		{
			condition.Initialize(gameObject);
		}

		foreach (var condition in _exitConditions)
		{
			condition.Initialize(gameObject);
		}

		foreach (var condition in _requiredConditions)
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
		if (_amountOfUsages.required)
		{
			AmountOfUsages--;
		}
		
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
		bool canEnter = _requiredConditions.Count == 0;

		foreach (var condition in _requiredConditions)
		{
			if (condition.DoChecks())
			{
				canEnter = true;
				break;
			}
		}

		if (!canEnter)
		{
			return false;
		}

		canEnter = _enterConditions.Count == 0;

		foreach (var condition in _enterConditions)
		{
			if (condition.DoChecks())
			{
				canEnter = true;
				break;
			}
		}

		return canEnter;
	}

	private bool DoExitChecks()
	{
		if (_duration.required && ActiveTime > Duration)
		{
			return true;
		}

		bool needExit = _requiredConditions.Count > 0;

		foreach (var condition in _requiredConditions)
		{
			if (condition.DoChecks())
			{
				needExit = false;
				break;
			}
		}

		if (needExit)
		{
			return true;
		}

		foreach (var condition in _exitConditions)
		{
			if (condition.DoChecks())
			{
				needExit = true;
				break;
			}
		}

		return needExit;
	}
}

[Serializable]
public struct AbilityParameter
{
	public float value;
	public bool required;
}

[Serializable]
public struct AbilityParameterInt
{
	public int value;
	public bool required;
}

[Serializable]
public struct BlockedAbility
{
	[SerializeField] private string _description;
	public Ability component;
}