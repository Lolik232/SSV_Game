using UnityEngine;

public class State : ScriptableObject
{
    protected StateMachine Machine;

    [SerializeField] protected string animBoolName;
    protected bool isActive;

    public virtual void OnStateEnter() => isActive = true;

    public virtual void OnStateExit() => isActive = false;

    public virtual void OnUpdate() { }

    public virtual void OnFixedUpdate() => DoChecks();

    protected virtual void DoChecks() { }
}

