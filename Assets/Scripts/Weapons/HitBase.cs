using System;

using UnityEngine;

public class HitBase : AnimatedComponentBase
{
	[HideInInspector] [NonSerialized] protected new HitSO component;

	protected override void Awake()
	{
		component = (HitSO)base.component;

		base.Awake();
	}
}
