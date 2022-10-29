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

	public override void InitialzeBase(GameObject baseObject)
	{
		base.InitialzeBase(baseObject);
		foreach (var element in elements)
		{
			element.InitialzeBase(baseObject);
		}
	}

	public override void InitializeParameters()
	{
		base.InitializeParameters();
		foreach (var element in elements)
		{
			element.InitializeParameters();
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
