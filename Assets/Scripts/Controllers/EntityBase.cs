using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AbilitiesManagerBase), typeof(ParametersManagerBase))]
[RequireComponent(typeof(CheckersManagerBase), typeof(InventoryBase), typeof(StateMachineBase))]
[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(TrailRenderer))]

public class EntityBase : ComponentBase
{
	[HideInInspector] protected new EntitySO component;

	protected override void Awake()
	{
		component = (EntitySO)base.component;

		base.Awake();
	}
}

	
