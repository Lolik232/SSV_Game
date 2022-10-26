using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public abstract class BaseMonoBehaviour : MonoBehaviour
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
		Utility.ApplyActions(startActions);
	}

	private void OnEnable()
	{
		Utility.ApplyActions(enableActions);
	}

	private void OnDisable()
	{
		Utility.ApplyActions(disableActions);
	}

	private void Update()
	{
		Utility.ApplyActions(updateActions);
	}

	private void FixedUpdate()
	{
		Utility.ApplyActions(fixedUpdateActions);
	}

	private void LateUpdate()
	{
		Utility.ApplyActions(lateUpdateActions);
	}

	private void OnDrawGizmos()
	{
		Utility.ApplyActions(drawGizmosActions);
	}
}
