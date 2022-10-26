using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManagerSO : ComponentSO
{
	[NonSerialized] public List<ComponentSO> elements;

	public override void InitialzeBase(GameObject baseObject)
	{
		base.InitialzeBase(baseObject);
		foreach (var element in elements)
		{
			element.InitialzeBase(baseObject);
		}
	}

	public override void InitializeParameters()
	{
		base.InitializeParameters();
		foreach (var element in elements)
		{
			element.InitializeParameters();
		}
	}
}
