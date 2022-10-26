using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public abstract class AbilitySO : ActionComponentSO, IBlockable
{
	[SerializeField] private List<BlockedState> _blockedStates = new();
	[SerializeField] private List<BlockedAbility> _blockedActions = new();

	private readonly Blocker _blocker = new();

	protected override void OnEnable()
	{
		base.OnEnable();

		enterActions.Add(() =>
		{
			Utility.BlockAll(_blockedActions);
			Utility.BlockAll(_blockedStates);
		});

		exitActions.Add(() =>
		{
			Utility.UnlockAll(_blockedActions);
			Utility.UnlockAll(_blockedStates);
		});

		enterConditions.Add(() =>
		{
			return !_blocker.IsLocked;
		});
	}

	public void Block(bool needHardExit)
	{
		if (needHardExit)
		{
			OnExit();
		}

		_blocker.AddBlock();
	}

	public void Unlock()
	{
		_blocker.RemoveBlock();
	}
}

[SerializeField]
public struct BlockedAbility
{
	public AbilitySO component;
	public bool needHardExit;

	public BlockedAbility(AbilitySO component, bool needHardExit)
	{
		this.component = component;
		this.needHardExit = needHardExit;
	}
}