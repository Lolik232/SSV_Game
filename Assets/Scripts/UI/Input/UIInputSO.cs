using System;
using System.Collections;
using System.Collections.Generic;

using All.Events;

using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "UI/Input/UIInput")]
public class UIInputSO : ScriptableObject
{
	[NonSerialized] public bool enterPressed;
	[NonSerialized] public bool escPressed;
	[NonSerialized] public bool optionsPressed;

	[SerializeField] private GameSceneSO locationToLoad;
	[SerializeField] private LoadEventChannelSO loadLocationChannel;

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

	public void OnQuitButton()
	{
		Application.Quit();
	}

	public void OnNewGameButton()
	{
		loadLocationChannel.RaiseEvent(locationToLoad, false, true);
	}
	
	public void OnOptionsButton()
	{
		optionsPressed = true;
	}

	public void OnOptionsBackButton()
	{
		escPressed = true;
	}
}