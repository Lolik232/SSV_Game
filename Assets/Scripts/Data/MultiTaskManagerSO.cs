using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "MultiTaskManager", menuName = "Data/Managers/Task Managers/Multiple")]

public class MultiTaskManagerSO : ManagerSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		updateActions.Add(() =>
		{
			bool entered = false;
			foreach (var el in elements)
			{
				if (!entered)
				{
					el.OnEnter();
					entered = true;
				}

				el.OnUpdate();
			}
		});

		fixedUpdateActions.Add(() =>
		{
			foreach (var el in elements)
			{
				el.OnFixedUpdate();
			}
		});

		lateUpdateActions.Add(() =>
		{
			foreach (var el in elements)
			{
				el.OnLateUpdate();
			}
		});

		drawGizmosActions.Add(() =>
		{
			foreach (var el in elements)
			{
				el.OnDrawGizmos();
			}
		});
	}
}
