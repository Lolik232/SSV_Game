using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class AbilitySO : ScriptableObject
{
	[SerializeField] private List<string> _animBoolNames = new();

	[SerializeField] private List<AbilitySO> _blockedAbilities = new();

	[Header("Parameters")]
	[SerializeField] protected float duration;
	[SerializeField] protected float cooldown;
	[SerializeField] private int _maxAmountOfUsages;

	protected float startTime;
	protected float endTime;

	protected int amountOfUsagesLeft;

	private int _amountOfBlocks;
	protected bool IsAble => _amountOfBlocks == 0;
	[NonSerialized] public bool isActive;

	protected List<UnityAction> useActions = new();
	protected List<UnityAction> terminateActions = new();
	protected List<UnityAction> updateActions = new();
	protected List<Func<bool>> useConditions = new();
	protected List<Func<bool>> terminateConditions = new();

	protected Animator Anim
	{
		get; private set;
	}

	protected virtual void OnEnable()
	{
		_amountOfBlocks = 0;
		isActive = false;
		amountOfUsagesLeft = _maxAmountOfUsages;
		startTime = 0f;
		endTime = 0f;

		useActions = new List<UnityAction> { () =>
		{
				isActive = true;
				amountOfUsagesLeft--;
				foreach (var ability in _blockedAbilities)
				{
						ability.Terminate();
						ability.Block();
				}

				foreach (var name in _animBoolNames)
				{
						Anim.SetBool(name, true);
				}

				startTime = Time.time;
		}};
		terminateActions = new List<UnityAction> { () =>
		{
				isActive = false;
				foreach (var ability in _blockedAbilities)
				{
						ability.Unlock();
				}

				foreach (var name in _animBoolNames)
				{
						Anim.SetBool(name, false);
				}

				endTime = Time.time;
		}};
		updateActions.Clear();
		useConditions = new List<Func<bool>> { () => 
		{
			return IsAble && 
			       !isActive &&
						 (_maxAmountOfUsages == 0 || amountOfUsagesLeft > 0) &&
						 Time.time > endTime + cooldown;
		}};
		terminateConditions = new List<Func<bool>> { () =>
		{
			return !isActive || 
						 (duration != 0 && Time.time > startTime + duration); 
		}};
	}

	protected void InitializeAnimator(Animator animator) => Anim = animator;

	public bool TryUseAbility()
	{
		bool canUse = true;
		foreach (var condition in useConditions)
		{
			canUse &= condition();
		}

		if (canUse)
		{
			foreach (var action in useActions)
			{
				action();
			}
		}

		return canUse;
	}

	public void OnUpdate()
	{
		foreach (var condition in terminateConditions)
		{
			if (!isActive)
			{
				return;
			}

			if (condition())
			{
				Terminate();
				return;
			}
		}

		foreach (var action in updateActions)
		{
			if (!isActive)
			{
				return;
			}

			action();
		}
	}

	public void Terminate()
	{
		if (!isActive)
		{
			return;
		}

		foreach (var action in terminateActions)
		{
			action();
		}
	}

	public void RestoreAmountOfUsages() => amountOfUsagesLeft = _maxAmountOfUsages;
	public void SetAmountOfUsagesToZero() => amountOfUsagesLeft = 0;
	public void DecreaseAmountOfUsages()
	{
		if (amountOfUsagesLeft > 0)
		{
			amountOfUsagesLeft--;
		}
	}
	public void Block() => _amountOfBlocks++;
	public void Unlock()
	{
		if (_amountOfBlocks > 0)
		{
			_amountOfBlocks--;
		}
	}
}
