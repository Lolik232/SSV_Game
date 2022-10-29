using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public abstract class ActionComponentSO : AnimatedComponentSO, IAnimated
{
	public Parameter duration;
	public Parameter cooldown;

	[SerializeField] protected Parameter amountOfUsages;

	protected List<Func<bool>> enterConditions = new();
	protected List<Func<bool>> exitConditions = new();

	protected List<UnityAction> prepareActions = new();

	protected override void OnEnable()
	{
		base.OnEnable();

		enterActions.Add(() =>
		{
			amountOfUsages.Current--;
			if (duration != 0)
			{
				anim.speed = 1 / duration;
			}
		});

		exitActions.Add(() =>
		{
			anim.speed = 1;
		});

		enterConditions = new List<Func<bool>> { () =>
		{
			return !isActive &&
						 (amountOfUsages.Max == 0 || amountOfUsages > 0) &&
						 InactiveTime > cooldown;
		}};

		exitConditions = new List<Func<bool>> { () =>
		{
			return !isActive ||
						 (duration.Max != 0 && ActiveTime > duration);
		}};

		prepareActions.Clear();
	}

	public override void Initialize(GameObject origin)
	{
		base.Initialize(origin);

		duration.Set(duration.Max);
		cooldown.Set(cooldown.Max);
		amountOfUsages.Set(amountOfUsages.Max);
	}

	public override void OnEnter()
	{
		if (CheckIfCanUse())
		{
			base.OnEnter();
		}
	}

	public override void OnUpdate()
	{
		CheckIfTerminate();
		base.OnUpdate();
	}

	private bool CheckIfCanUse()
	{
		Utility.ApplyActions(prepareActions);

		foreach (var condition in enterConditions)
		{
			if (!condition())
			{
				return false;
			}
		}

		return true;
	}

	private void CheckIfTerminate()
	{
		foreach (var condition in exitConditions)
		{
			if (condition())
			{
				OnExit();
				return;
			}
		}
	}

	public void RestoreAmountOfUsages() => amountOfUsages.Set(amountOfUsages.Max);

	public void SetAmountOfUsagesToZero() => amountOfUsages.Set(0);

	public void DecreaseAmountOfUsages()
	{
		if (amountOfUsages > 0)
		{
			amountOfUsages.Current--;
		}
	}
}
