using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManagerBase<T> : ComponentBase
{
	[HideInInspector] protected new ManagerSO<T> component;

	protected override void Awake()
	{
		component = (ManagerSO<T>)base.component;

		base.Awake();
	}
}
