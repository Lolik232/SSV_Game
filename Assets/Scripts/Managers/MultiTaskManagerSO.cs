using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class MultiTaskManagerSO<T> : StaticManagerSO<T> where T : ComponentSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		updateActions.Add(() =>
		{
			bool entered = false;
			for (int i = 0; i < elements.Count; i++)
			{
				if (!entered)
				{
					if (elements[i] != null)
					{
						elements[i].OnEnter();
						entered = elements[i].isActive;
					}

				}

				elements[i]?.OnUpdate();
			}
		});

		fixedUpdateActions.Add(() =>
		{
			for (int i = 0; i < elements.Count; i++)
			{
				elements[i]?.OnFixedUpdate();
			}
		});

		lateUpdateActions.Add(() =>
		{
			for (int i = 0; i < elements.Count; i++)
			{
				elements[i]?.OnLateUpdate();
			}
		});

		drawGizmosActions.Add(() =>
		{
			for (int i = 0; i < elements.Count; i++)
			{
				elements[i]?.OnDrawGizmos();
			}
		});
	}
	public override void InitialzeBase(GameObject baseObject)
	{
		base.InitialzeBase(baseObject);
		for (int i = 0; i < elements.Count; i++)
		{
			elements[i]?.InitialzeBase(baseObject);
		}
	}

	public override void InitializeParameters()
	{
		base.InitializeParameters();
		for (int i = 0; i < elements.Count; i++)
		{
			elements[i]?.InitializeParameters();
		}
	}
}
