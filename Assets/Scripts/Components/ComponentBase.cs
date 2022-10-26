using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ComponentBase : BaseMonoBehaviour
{
	[SerializeField] protected ComponentSO component;

	protected override void Awake()
	{
		component.InitialzeBase(gameObject);
		component.InitializeParameters();

		base.Awake();

		enableActions.Add(() =>
		{
			component.OnEnter();
		});

		disableActions.Add(() =>
		{
			component.OnExit();
		});

		updateActions.Add(() =>
		{
			component.OnUpdate();
		});

		lateUpdateActions.Add(() =>
		{
			component.OnLateUpdate();
		});

		fixedUpdateActions.Add(() =>
		{
			component.OnFixedUpdate();
		});

		drawGizmosActions.Add(() =>
		{
			component.OnDrawGizmos();
		});
	}
}
