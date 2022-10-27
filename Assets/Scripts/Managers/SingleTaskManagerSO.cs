using UnityEngine;

public abstract class SingleTaskManagerSO<T> : ManagerSO<T>, IAnimated where T : AnimatedComponentSO
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

	public override void InitialzeBase(GameObject baseObject)
	{
		base.InitialzeBase(baseObject);
		for (int i = 0; i < elements.Count; i++)
		{
			elements[i].InitialzeBase(baseObject);
		}
	}

	public override void InitializeParameters()
	{
		base.InitializeParameters();
		for (int i = 0; i < elements.Count; i++)
		{
			elements[i].InitializeParameters();
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
