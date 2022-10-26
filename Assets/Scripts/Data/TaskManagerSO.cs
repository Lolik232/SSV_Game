using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskManagerSO : ManagerSO
{
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
