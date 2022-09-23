using All.Events;

using UnityEngine;

[RequireComponent(typeof(Animator))]

public class StateMachine : MonoBehaviour
{
    [SerializeField] private StateSO _defaultState;
    private StateSO _currentState = null;

    [SerializeField] private StateChangeEventChannelSO _stateChangeListener;

    private Animator _anim;

    protected virtual void Awake()
    {
        _anim = GetComponent<Animator>();
        _stateChangeListener.OnEventRaised += GetTransitionState;
    }

    protected virtual void Start()
    {
        GetTransitionState(_defaultState);
    }

    private void Update()
    {
        _currentState.OnUpdate();
    }

    private void FixedUpdate()
    {
        _currentState.OnFixedUpdate();
    }

    private void GetTransitionState(StateSO transitionState)
    {
        if (_currentState != null)
        {
            _currentState.OnStateExit();
            _anim.SetBool(_currentState.AnimBoolName, false);
        }
        _currentState = transitionState;
        _currentState.OnStateEnter();
        _anim.SetBool(_currentState.AnimBoolName, true);

    }
}
