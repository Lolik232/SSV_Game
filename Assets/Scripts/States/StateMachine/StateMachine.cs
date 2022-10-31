using UnityEngine;

public class StateMachine : BaseMonoBehaviour
{
	[SerializeField] private TransitionTableSO _transitions;

	private TransitionItem _currentTransition;

	public StateSO Current
	{
		get => _currentTransition.origin;
	} 

	protected override void Awake()
	{
		base.Awake();
		_transitions.Initialize(gameObject);
	}

	protected override void Update()
	{
		base.Update();
		_transitions.TryGetTransition(ref _currentTransition);
		_currentTransition.origin.OnUpdate();
	}
}
