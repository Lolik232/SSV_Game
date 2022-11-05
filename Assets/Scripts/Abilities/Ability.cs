using System;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(AbilitiesManager))]

public abstract class Ability : MonoBehaviour
{
	[SerializeField] private List<AnimationBool> _animBools;
	[Space]
	[SerializeField] private List<BlockedAbility> _blockedAbilities;

	protected List<Func<bool>> enterConditions = new();
	protected List<Func<bool>> exitConditions = new();

	private readonly Blocker _blocker = new();

	protected Animator anim;

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

	protected virtual void Awake()
	{
		anim = GetComponent<Animator>();

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

		if (!DoEnterChecks())
		{
			return;
		}

		ApplyEnterActions();
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
		Utility.BlockAll(_blockedAbilities);
		Utility.SetAnimBoolsOnEnter(anim, _animBools);
		_startTime = Time.time;
	}

	protected virtual void ApplyExitActions()
	{
		IsActive = false;
		Utility.UnlockAll(_blockedAbilities);
		Utility.SetAnimBoolsOnExit(anim, _animBools);
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
public struct BlockedAbility
{
	[SerializeField] private string _description;
	public Ability component;
}