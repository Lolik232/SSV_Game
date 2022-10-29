using System;

using UnityEngine;

public class PlayerInputReaderBase : ComponentBase
{
	[HideInInspector] [NonSerialized] protected new PlayerInputReaderSO component;

	protected override void Awake()
	{
		component = (PlayerInputReaderSO)base.component;

		base.Awake();
	}
}