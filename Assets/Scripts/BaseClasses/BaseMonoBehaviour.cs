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
		ApplyActions(startActions);
	}

	private void OnEnable()
	{
		ApplyActions(enableActions);
	}

	private void OnDisable()
	{
		ApplyActions(disableActions);
	}

	private void Update()
	{
		ApplyActions(updateActions);
	}

	private void FixedUpdate()
	{
		ApplyActions(fixedUpdateActions);
	}

	private void LateUpdate()
	{
		ApplyActions(lateUpdateActions);
	}

	private void OnDrawGizmos()
	{
		ApplyActions(drawGizmosActions);
	}

	protected void ApplyActions(List<UnityAction> actions)
	{
		foreach (var action in actions)
		{
			action();
		}
	}
}
