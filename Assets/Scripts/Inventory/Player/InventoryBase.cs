using System;

using UnityEngine;

public class InventoryBase : ComponentBase
{
	[HideInInspector] [NonSerialized] protected new InventorySO component;

	protected override void Awake()
	{
		component = (InventorySO)base.component;

		base.Awake();
	}
}
