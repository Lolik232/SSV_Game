using UnityEngine;

public class MultiTaskManagerBase<T> : ComponentBase where T : ComponentSO
{
	[HideInInspector] protected new MultiTaskManagerSO<T> component;

	protected override void Awake()
	{
		component = (MultiTaskManagerSO<T>)base.component;

		base.Awake();
	}
}
