using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class CheckersManagerBase : ComponentBase
{
	[HideInInspector] protected new CheckersManagerSO component;

	protected override void Awake()
	{
		component = (CheckersManagerSO)base.component;

		base.Awake();
	}
}
