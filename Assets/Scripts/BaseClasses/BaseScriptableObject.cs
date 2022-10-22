using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseScriptableObject : ScriptableObject
{
	protected List<UnityAction> enterActions = new();
	protected List<UnityAction> exitActions = new();
	protected List<UnityAction> updateActions = new();
	protected List<UnityAction> fixedUpdateActions = new();
	protected List<UnityAction> lateUpdateActions = new();

	protected virtual void OnEnable()
	{
		enterActions.Clear();
		exitActions.Clear();
		updateActions.Clear();
		fixedUpdateActions.Clear();
		lateUpdateActions.Clear();
	}

	protected void ApplyActions(List<UnityAction> actions)
	{
		foreach (var action in actions)
		{
			action();
		}
	}

	protected void ApplyActions<T>(List<UnityAction<T>> actions, T value)
	{
		foreach (var action in actions)
		{
			action(value);
		}
	}
}
