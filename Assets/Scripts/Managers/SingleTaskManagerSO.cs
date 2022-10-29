using UnityEngine;

public abstract class SingleTaskManagerSO<T> : StaticManagerSO<T>, IAnimated where T : AnimatedComponentSO
{
	[SerializeField] private T _default;

	public T Current
	{
		get; private set;
	}

	protected override void OnEnable()
	{
		base.OnEnable();

		enterActions.Add(() =>
		{
			GetNext(_default);
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

	public override void Initialize(GameObject origin)
	{
		base.Initialize(origin);
		foreach (var element in elements)
		{
			element.Initialize(origin);
		}
	}

	public void GetNext(T next)
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
