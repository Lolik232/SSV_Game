using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public abstract class AbilitySO : BaseScriptableObject
{
	[SerializeField] protected Parameter duration;
	[SerializeField] protected Parameter cooldown;
	[SerializeField] private int _maxAmountOfUsages;

	[SerializeField] protected DataSO data;
	[SerializeField] protected EntitySO entity;

	[SerializeField] private List<string> _animBoolNames = new();
	[SerializeField] private List<BlockedAbility> _blockedAbilities = new();
	[SerializeField] private List<BlockedState> _blockedStates = new();

	protected List<UnityAction> beforeEnterActions = new();
	protected List<Func<bool>> useConditions = new();
	protected List<Func<bool>> terminateConditions = new();

	protected float startTime;
	protected float endTime;
	protected int amountOfUsagesLeft;
	public Blocker blocker = new();

	protected Animator anim;

	[NonSerialized] public bool isActive;

	protected override void OnEnable()
	{
		isActive = false;

		amountOfUsagesLeft = _maxAmountOfUsages;

		startTime = 0f;
		endTime = 0f;

		duration.Set(duration.Max);
		cooldown.Set(cooldown.Max);

		base.OnEnable();

		beforeEnterActions.Clear();
		enterActions = new List<UnityAction> { () =>
		{
			isActive = true;
			amountOfUsagesLeft--;

			BlockStates();
			BlockAbilities();

			foreach (var name in _animBoolNames)
			{
				anim.SetBool(name, true);
			}

			startTime = Time.time;
		}};

		exitActions = new List<UnityAction> { () =>
		{
			isActive = false;

      UnlockStates();
			UnlockAbilities();

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

	public void Initialize(Animator animator) => anim = animator;

	public bool TryUseAbility()
	{
		ApplyActions(beforeEnterActions);

		bool canUse = true;
		foreach (var condition in useConditions)
		{
			canUse &= condition();
		}

		if (canUse)
		{
			ApplyActions(enterActions);
		}

		return canUse;
	}

	public void OnUpdate()
	{
		if (!isActive)
		{
			return;
		}

		foreach (var condition in terminateConditions)
		{
			if (condition())
			{
				Terminate();
				return;
			}
		}

		ApplyActions(updateActions);
	}

	public void Terminate()
	{
		if (!isActive)
		{
			return;
		}

		ApplyActions(exitActions);
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

	private void BlockStates()
	{
		foreach (var state in _blockedStates)
		{
			state.blockedState.SetBlockTransition(state.transitionState);
			state.blockedState.blocker.AddBlock();
		}
	}

	private void UnlockStates()
	{
		foreach (var state in _blockedStates)
		{
			state.blockedState.ResetBlockTransition();
			state.blockedState.blocker.RemoveBlock();
		}
	}

	private void BlockAbilities()
	{
		foreach (var ability in _blockedAbilities)
		{
			if (ability.isHardBlocked)
			{
				ability.blockedAbility.Terminate();
			}

			ability.blockedAbility.blocker.AddBlock();
		}
	}

	private void UnlockAbilities()
	{
		foreach (var ability in _blockedAbilities)
		{
			ability.blockedAbility.blocker.RemoveBlock();
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
