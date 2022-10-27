using System;
using System.Security;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReaderBase : ComponentBase
{
	[HideInInspector] protected new PlayerInputReaderSO component;

	protected override void Awake()
	{
		component = (PlayerInputReaderSO)base.component;

		base.Awake();
	}
}