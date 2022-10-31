using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "State", menuName = "States/State")]

public class StateSO : BaseScriptableObject, IComponent, IBlockable
{
	[SerializeField] private List<AnimationBool> _animBools;
	[Space]
	[SerializeField] private List<PermitedAbility> _permitedAbilities;
	[Space]

	protected List<TransitionItem> transitions = new();

	public StateSO blockedTransition;

	private readonly Blocker _blocker = new();

	protected Animator anim;

	public bool IsLocked
	{
		get => _blocker.IsLocked;
	}

	protected override void OnEnable()
	{
		base.OnEnable();

		enterActions.Add(() =>
		{
			Utility.UnlockAll(_permitedAbilities);
			Utility.SetAnimBoolsOnEnter(anim, _animBools);
		});

		exitActions.Add(() =>
		{
			Utility.BlockAll(_permitedAbilities);
			Utility.SetAnimBoolsOnExit(anim, _animBools);
		});
	}

	public virtual void Initialize(GameObject origin)
	{
		anim = origin.GetComponent<Animator>();
	}

	public void SetBlockedTransition(StateSO blockedTransition)
	{
		this.blockedTransition = blockedTransition;
	}

	public void Block()
	{
		_blocker.AddBlock();
	}

	public void Unlock()
	{
		blockedTransition = null;
		_blocker.RemoveBlock();
	}
}

[Serializable]
public struct BlockedState
{
	public StateSO component;
	public StateSO target;

	public BlockedState(StateSO component, StateSO target)
	{
		this.component = component;
		this.target = target;
	}
}

[Serializable]
public struct AnimationBool
{
	public enum EnableMode
	{
		Enable, 
		Disable, 
		EnableOnEnterDisableOnExit,
		DisableOnEnterEnableOnExit
	} 

	public string name;
	public EnableMode mode;

	public AnimationBool(string name, EnableMode enable)
	{
		this.name = name;
		this.mode = enable;
	}
}
