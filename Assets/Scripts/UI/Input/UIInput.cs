using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "UI/Input/UIInput")]
public class UIInput : ScriptableObject
{
	[NonSerialized] public bool enterPressed;
	[NonSerialized] public bool escPressed;

	public void OnEnterDown(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			enterPressed = true;
		}
		else if (context.canceled)
		{
			enterPressed = false;
		}
	}

	public void OnEscDown(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			escPressed = true;
		}
		else if (context.canceled)
		{
			escPressed = false;
		}
	}
}