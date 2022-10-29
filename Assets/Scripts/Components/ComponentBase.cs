using UnityEngine;

public abstract class ComponentBase : BaseMonoBehaviour
{
	[SerializeField] protected ComponentSO component;

	protected override void Awake()
	{
		base.Awake();
		component.Initialize(gameObject);
	}

	protected override void Start()
	{
		base.Start();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		component.OnEnter();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		component.OnExit();
	}

	protected override void Update()
	{
		base.Update();
		component.OnUpdate();
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		component.OnFixedUpdate();
	}

	protected override void LateUpdate()
	{
		base.LateUpdate();
		component.OnLateUpdate();
	}

	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
		component.OnDrawGizmos();
	}
}
