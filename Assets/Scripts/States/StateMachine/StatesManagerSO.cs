using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesManagerSO : ScriptableObject
{
	[NonSerialized] public List<PlayerStateSO> states;
	public void Initialize(Animator anim)
	{
		foreach (var state in states)
		{
			state.Initialize(anim);
		}
	}
}
