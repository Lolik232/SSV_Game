using System;
using System.Collections.Generic;

using UnityEngine;

public abstract class AbilitySO : ActionComponentSO, IBlockable
{
	[SerializeField] private List<BlockedState> _blockedStates = new();
	[SerializeField] private List<PermitedAbility> _blockedAbilities = new();

	private readonly Blocker _blocker = new();

	public bool IsLocked
	{
		get => _blocker.IsLocked;
	}

	protected override void OnEnable()
	{
		base.OnEnable();

		enterActions.Add(() =>
		{
			Utility.BlockAll(_blockedAbilities);
			Utility.BlockAll(_blockedStates);
		});

		exitActions.Add(() =>
		{
			Utility.UnlockAll(_blockedAbilities);
			Utility.UnlockAll(_blockedStates);
		});

		enterConditions.Add(() =>
		{
			return !IsLocked;
		});
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
}

[Serializable]
public struct PermitedAbility
{
	public AbilitySO component;
	public bool needHardExit;

	public PermitedAbility(AbilitySO component, bool needHardExit)
	{
		this.component = component;
		this.needHardExit = needHardExit;
	}
}