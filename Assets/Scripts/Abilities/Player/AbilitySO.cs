using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public abstract class AbilitySO : ScriptableObject
{
	[SerializeField] protected Parameter duration;
	[SerializeField] protected Parameter cooldown;
	[SerializeField] private int _maxAmountOfUsages;

	[SerializeField] private List<string> _animBoolNames = new();
	[SerializeField] private List<BlockedAbility> _blockedAbilities = new();
	[SerializeField] private List<BlockedState> _blockedStates = new();

	protected List<UnityAction> beforeUseActions = new();
	protected List<UnityAction> useActions = new();
	protected List<UnityAction> terminateActions = new();
	protected List<UnityAction> updateActions = new();
	protected List<Func<bool>> useConditions = new();
	protected List<Func<bool>> terminateConditions = new();

	protected float startTime;
	protected float endTime;
	protected int amountOfUsagesLeft;
	public Blocker blocker = new();

	protected Animator anim;

	[NonSerialized] public bool isActive;

	protected virtual void OnEnable()
	{
		isActive = false;

		amountOfUsagesLeft = _maxAmountOfUsages;

		startTime = 0f;
		endTime = 0f;

		duration.Set(duration.Max);
		cooldown.Set(cooldown.Max);

		beforeUseActions.Clear();
		updateActions.Clear();
		useActions = new List<UnityAction> { () =>
		{
			isActive = true;
			amountOfUsagesLeft--;
			foreach (var ability in _blockedAbilities)
			{
				if (ability.isHardBlocked)
				{
					ability.blockedAbility.Terminate();
				}

				ability.blockedAbility.blocker.AddBlock();
			}

			foreach (var state in _blockedStates)
			{
				state.blockedState.SetBlockTransition(state.transitionState);
				state.blockedState.blocker.AddBlock();
			}

			foreach (var name in _animBoolNames)
			{
				anim.SetBool(name, true);
			}

			startTime = Time.time;
		}};
		terminateActions = new List<UnityAction> { () =>
		{
			isActive = false;

			foreach (var ability in _blockedAbilities)
			{
				ability.blockedAbility.blocker.RemoveBlock();
			}

			foreach (var state in _blockedStates)
			{
				state.blockedState.ResetBlockTransition();
				state.blockedState.blocker.RemoveBlock();
			}

			foreach (var name in _animBoolNames)
			{
				anim.SetBool(name, false);
			}

			endTime = Time.time;
		}};

		useConditions = new List<Func<bool>> { () =>
		{
			return !blocker.IsLocked &&
						 !isActive &&
						 (_maxAmountOfUsages == 0 || amountOfUsagesLeft > 0) &&
						 Time.time > endTime + cooldown;
		}};

		terminateConditions = new List<Func<bool>> { () =>
		{
			return !isActive ||
						 (duration.Max != 0 && Time.time > startTime + duration);
		}};
	}

	protected void InitializeAnimator(Animator animator) => anim = animator;

	public bool TryUseAbility()
	{
		foreach (var action in beforeUseActions)
		{
			action();
		}

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
}

[Serializable]
public struct BlockedAbility
{
	public AbilitySO blockedAbility;
	public bool isHardBlocked;

	public BlockedAbility(AbilitySO blockedAbility, bool isHardBlocked)
	{
		this.blockedAbility = blockedAbility;
		this.isHardBlocked = isHardBlocked;
	}
}
