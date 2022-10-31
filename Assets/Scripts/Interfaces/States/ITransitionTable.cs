using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransitionTable
{
	public StateSO Default
	{
		get;
	} 
	public List<TransitionItem> Transitions
	{
		get;
	}

	public void TryGetTransition(ref TransitionItem transition);
}
