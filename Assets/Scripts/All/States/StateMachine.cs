using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private State _defaultState;
    private State _currentState;

    protected virtual void Awake()
    {
        _currentState = _defaultState;
    }

    protected virtual void Start()
    {
        _currentState.OnStateEnter();
    }

    private void Update()
    {
        _currentState.OnUpdate();
    }

    private void FixedUpdate()
    {
        _currentState.OnFixedUpdate();
    }

    public void GetTransitionState(State transitionState)
    {
        _currentState.OnStateExit();
        _currentState = transitionState;
        _currentState.OnStateEnter();
    }
}
