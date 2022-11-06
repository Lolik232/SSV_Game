using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityState<AbilityT> : StateBase where AbilityT : Ability
{
	protected AbilityT Ability
	{
		get;
		private set;
	}
	public List<TransitionItem<AbilityState<AbilityT>>> Transitions
	{
		get;
		protected set;
	} = new();

	protected override void Awake()
	{
		base.Awake();
		Ability = GetComponent<AbilityT>();
	}

	protected override void TryGetTransition()
	{
		foreach (var transition in Transitions)
		{
			if (transition.condition())
			{
				transition.action?.Invoke();
				Ability.GetTransition(transition.target);
				return;
			}
		}
	}
}
