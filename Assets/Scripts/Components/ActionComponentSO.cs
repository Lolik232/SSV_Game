using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class ActionComponentSO : AnimatedComponentSO, IAnimated
{
	[SerializeField] protected Parameter duration;
	[SerializeField] protected Parameter cooldown;
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
		});

		enterConditions = new List<Func<bool>> { () =>
		{
			return !isActive &&
						 (amountOfUsages.Max == 0 || amountOfUsages > 0) &&
						 Time.time > endTime + cooldown;
		}};

		exitConditions = new List<Func<bool>> { () =>
		{
			return !isActive ||
						 (duration.Max != 0 && Time.time > startTime + duration);
		}};

		prepareActions.Clear();
	}

	public override void InitializeParameters()
	{
		base.InitializeParameters();

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

		for (int i = 0; i < enterConditions.Count; i++)
		{
			if (!enterConditions[i]())
			{
				return false;
			}
		}

		return true;
	}

	private void CheckIfTerminate()
	{
		for (int i = 0; i < exitConditions.Count; i++)
		{
			if (exitConditions[i]())
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
