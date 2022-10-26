using System;
using System.Collections;
using System.Collections.Generic;

using Unity.IO.LowLevel.Unsafe;

using UnityEngine;

[CreateAssetMenu(fileName = "SingleTaskManager", menuName = "Data/Managers/Task Managers/Single")]

public class SingleTaskManagerSO : ManagerSO, IAnimated
{
	[SerializeField] private AnimatedComponentSO _default;

	public AnimatedComponentSO Current
	{
		get; private set;
	}

	protected override void OnEnable()
	{
		base.OnEnable();

		enterActions.Add(() =>
		{
			GetNext(default);
		});

		updateActions.Add(() =>
		{
			Current.OnUpdate();
		});

		fixedUpdateActions.Add(() =>
		{
			Current.OnFixedUpdate();
		});

		lateUpdateActions.Add(() =>
		{
			Current.OnLateUpdate();
		});

		drawGizmosActions.Add(() =>
		{
			Current.OnDrawGizmos();
		});
	}

	public void GetNext(AnimatedComponentSO next)
	{
		if (Current != null)
		{
			Current.OnExit();
		}

		Current = next;
		Current.OnEnter();
	}

	public void OnAnimationTrigger()
	{
		Current.OnAnimationTrigger();
	}

	public void OnAnimationFinishTrigger()
	{
		Current.OnAnimationFinishTrigger();
	}
}
