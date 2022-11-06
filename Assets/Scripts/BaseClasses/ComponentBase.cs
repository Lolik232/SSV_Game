using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;

using UnityEngine;

public abstract class ComponentBase : MonoBehaviour
{
	private float _startTime;
	private float _endTime;

	public bool IsActive
	{
		get;
		protected set;
	}
	public float ActiveTime
	{
		get => Time.time - _startTime;
		private set => _startTime = value;
	}
	public float InactiveTime
	{
		get => Time.time - _endTime;
		private set => _endTime = value;
	}

	public abstract void OnEnter();

	public abstract void OnExit();

	public abstract void OnUpdate();

	protected virtual void ApplyEnterActions()
	{
		IsActive = true;
		ActiveTime = Time.time;
	}

	protected virtual void ApplyExitActions()
	{
		IsActive = false;
		InactiveTime = Time.time;
	}

	protected virtual void ApplyUpdateActions()
	{

	}
}
