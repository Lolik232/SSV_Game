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
	[SerializeField] private List<BlockedAbility> _blockedAbilities;

	protected List<Func<bool>> enterConditions = new();
	protected List<Func<bool>> exitConditions = new();

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

		if (_duration.required && ActiveTime > Duration || DoExitChecks())
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
		Utility.SetAnimBoolsOnEnter(_anim, _animBools);
		_startTime = Time.time;
	}

	protected virtual void ApplyExitActions()
	{
		IsActive = false;
		Utility.UnlockAll(_blockedAbilities);
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
		foreach (var condition in enterConditions)
		{
			if (condition())
			{
				return true;
			}
		}

		return false;
	}

	private bool DoExitChecks()
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