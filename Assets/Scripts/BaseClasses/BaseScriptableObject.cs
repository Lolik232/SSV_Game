using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseScriptableObject : ScriptableObject
{
	protected List<UnityAction> enterActions = new();
	protected List<UnityAction> exitActions = new();
	protected List<UnityAction> updateActions = new();
	protected List<UnityAction> fixedUpdateActions = new();
	protected List<UnityAction> lateUpdateActions = new();
	protected List<UnityAction> drawGizmosActions = new();

	protected float startTime;
	protected float endTime;

	[NonSerialized] public bool isActive;

	protected virtual void OnEnable()
	{
		startTime = 0f;
		endTime = 0f;
		isActive = false;

		enterActions = new List<UnityAction>
		{
			() =>
			{
				isActive = true;
				startTime = Time.time;
			}
		};

		exitActions = new List<UnityAction>
		{
			() =>
			{
				isActive = false;
				endTime = Time.time;
			}
		};

		updateActions.Clear();
		fixedUpdateActions.Clear();
		lateUpdateActions.Clear();
		drawGizmosActions.Clear();
	}

	public virtual void OnEnter()
	{
		if (!isActive)
		{
			Utility.ApplyActions(enterActions);
		} 
	}

	public virtual void OnExit()
	{
		if (isActive)
		{
			Utility.ApplyActions(exitActions);
		}
	}

	public virtual void OnUpdate()
	{
		if (isActive)
		{
			Utility.ApplyActions(updateActions);
		}
	}

	public virtual void OnLateUpdate()
	{
		if (isActive)
		{
			Utility.ApplyActions(lateUpdateActions);
		}
	}

	public virtual void OnFixedUpdate()
	{
		if (isActive)
		{
			Utility.ApplyActions(fixedUpdateActions);
		}
	}

	public virtual void OnDrawGizmos()
	{
		if (isActive)
		{
			Utility.ApplyActions(drawGizmosActions);
		}
	}
}
