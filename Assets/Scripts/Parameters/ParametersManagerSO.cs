using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParametersManagerSO : ScriptableObject
{
	[NonSerialized] public List<Parameter> parameters = new();

	public void Initialize()
	{
		foreach (var parameter in parameters)
		{
			parameter.Set(parameter.Max);
		}
	}
}
