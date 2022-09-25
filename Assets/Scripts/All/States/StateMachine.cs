using All.Events;

using Unity.VisualScripting;

using UnityEngine;

[RequireComponent(typeof(Animator))]

public class StateMachine : MonoBehaviour
{
    [SerializeField] private SubStateSO _defaultState;
    private SubStateSO _currentState = null;

    private Animator _anim;

    protected virtual void Awake()
    {
        _anim = GetComponent<Animator>();
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

    private void OnAnimationFinishTrigger() => _currentState.OnAnimationFinishTrigger();

    private void OnAnimationTrigger() => _currentState.OnAnimationTrigger();

    public void GetTransitionState(SubStateSO transitionState)
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
