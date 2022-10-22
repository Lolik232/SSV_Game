using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManagerSO : ScriptableObject
{
	[NonSerialized] public List<PlayerAbilitySO> abilities;

	public void Initialize(Animator anim)
	{
		foreach (var ability in abilities)
		{
			ability.Initialize(anim);
		}
	}
}
