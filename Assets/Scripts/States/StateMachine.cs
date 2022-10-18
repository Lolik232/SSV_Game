using UnityEngine;

[RequireComponent(typeof(Animator))]

public class StateMachine : MonoBehaviour
{
	[SerializeField] private PlayerStatesManagerSO _statesManager;
	[SerializeField] private StateSO _defaultState;

	private StateSO _currentState;
	private Player _player;
	protected virtual void Awake()
	{
		_player = GetComponent<Player>();
	}

	protected virtual void Start()
	{
		_statesManager.Initialize(_player);
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

	private void OnStateAnimationFinishTrigger() => _currentState.OnStateAnimationFinishTrigger();

	private void OnStateAnimationTrigger() => _currentState.OnStateAnimationTrigger();

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
