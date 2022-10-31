using System;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

[CreateAssetMenu(fileName = "TransitionTable", menuName = "States/Table")]

public class TransitionTableSO : ScriptableObject, IComponent, ITransitionTable
{
	[SerializeField] private StateSO _default;
	[SerializeField] private List<TransitionItem> _transitions;

	public List<TransitionItem> Transitions
	{
		get => _transitions;
	}
	public StateSO Default
	{
		get => _default;
	}

	public void Initialize(GameObject origin)
	{
		foreach (var transition in _transitions)
		{
			transition.Initialize(origin);
		}
	}

	public void TryGetTransition(ref TransitionItem transition)
	{
		if (!transition.origin.isActive)
		{
			return;
		}

		if (transition.origin.IsLocked)
		{
			GetTransition(transition.origin.blockedTransition, ref transition);
			return;
		}

		foreach (var target in transition.targets)
		{
			if (target.DoChecks())
			{
				GetTransition(target.target, ref transition);
				return;
			}
		}
	}

	private void GetTransition(StateSO target, ref TransitionItem transition)
	{
		transition.origin.OnExit();
		Find(target, ref transition);
		transition.origin.OnEnter();
	}

	private void Find(StateSO state, ref TransitionItem item) 
	{
		foreach (var transition in _transitions)
		{
			if (transition.origin.Equals(state))
			{
				item = transition;
			}
		}

		throw new Exception("State \"" + state.ToString() + "\" Doesn`t exist");
	}
}

[Serializable]
public struct TransitionItem : IComponent
{
	public StateSO origin;
	public List<TransitionTarget> targets;

	public TransitionItem(StateSO origin, List<TransitionTarget> targets)
	{
		this.origin = origin;
		this.targets = targets;
	}

	public void Initialize(GameObject origin)
	{
		this.origin.Initialize(origin);
		foreach (var target in targets)
		{
			target.Initialize(origin);
		}
	}
}

[Serializable]
public struct TransitionTarget : IComponent, ICondition
{
	public StateSO target;
	public List<Condition> conditions;

	public TransitionTarget(StateSO target, List<Condition> conditions)
	{
		this.target = target;
		this.conditions = conditions;
	}

	public bool DoChecks()
	{
		foreach (var condition in conditions)
		{
			if (condition.DoChecks())
			{
				return true;
			}
		}

		return true;
	}

	public void Initialize(GameObject origin)
	{
		target.Initialize(origin);
		foreach (var condition in conditions)
		{
			condition.Initialize(origin);
		}
	}
}