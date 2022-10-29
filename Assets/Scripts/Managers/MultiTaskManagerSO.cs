using UnityEngine;

public abstract class MultiTaskManagerSO<T> : StaticManagerSO<T> where T : ComponentSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		updateActions.Add(() =>
		{
			bool entered = false;
			foreach (var element in elements)
			{
				if (!entered)
				{
					if (element != null)
					{
						element.OnEnter();
						entered = element.isActive;
					}

				}

				element?.OnUpdate();
			}
		});

		fixedUpdateActions.Add(() =>
		{
			foreach (var element in elements)
			{
				element?.OnFixedUpdate();
			}
		});

		lateUpdateActions.Add(() =>
		{
			foreach (var element in elements)
			{
				element?.OnLateUpdate();
			}
		});

		drawGizmosActions.Add(() =>
		{
			foreach (var element in elements)
			{
				element?.OnDrawGizmos();
			}
		});
	}
	public override void Initialize(GameObject origin)
	{
		base.Initialize(origin);
		foreach (var element in elements)
		{
			element?.Initialize(origin);
		}
	}
}
