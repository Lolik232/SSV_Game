using UnityEngine;

[RequireComponent(typeof(Animator))]

public class StateMachine : MonoBehaviour
{
	[SerializeField] private StateSO _defaultState;
	private StateSO _currentState = null;

	protected virtual void Awake()
	{
	}

	protected virtual void Start()
	{
		GetTransitionState(_defaultState);
	}

	private void Update()
	{
		_currentState.OnStateUpdate();
	}

	private void FixedUpdate()
	{
		_currentState.OnFixedUpdate();
	}

	private void OnAnimationFinishTrigger() => _currentState.OnAnimationFinishTrigger();

	private void OnAnimationTrigger() => _currentState.OnAnimationTrigger();

	public void GetTransitionState(StateSO transitionState)
	{
		if (_currentState != null)
		{
			_currentState.OnStateExit();
		}

		_currentState = transitionState;
		_currentState.OnStateEnter();
	}
}
