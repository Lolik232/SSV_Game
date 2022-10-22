using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BaseMonoBehaviour : MonoBehaviour
{
	protected List<UnityAction> startActions = new();
	protected List<UnityAction> enableActions = new();
	protected List<UnityAction> disableActions = new();
	protected List<UnityAction> updateActions = new();
	protected List<UnityAction> lateUpdateActions = new();
	protected List<UnityAction> fixedUpdateActions = new();
	protected List<UnityAction> drawGizmosActions = new();

	protected virtual void Awake()
	{
		
	}

	private void Start()
	{
		foreach (var action in startActions)
		{
			action();
		}
	}

	private void OnEnable()
	{
		foreach (var action in enableActions)
		{
			action();
		}
	}

	private void OnDisable()
	{
		foreach (var action in disableActions)
		{
			action();
		}
	}

	private void Update()
	{
		foreach (var action in updateActions)
		{
			action();
		}
	}

	private void FixedUpdate()
	{
		foreach (var action in fixedUpdateActions)
		{
			action();
		}
	}

	private void LateUpdate()
	{
		foreach (var action in lateUpdateActions)
		{
			action();
		}
	}

	private void OnDrawGizmos()
	{
		foreach (var action in drawGizmosActions)
		{
			action();
		}
	}
}
