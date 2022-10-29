using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerSO : BaseScriptableObject, IComponent
{
	protected List<IComponent> components = new();

	public void Initialize(GameObject origin)
	{
		foreach (var component in components)
		{
			component.Initialize(origin);
		}
	}
}
