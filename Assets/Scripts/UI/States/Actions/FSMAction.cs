using UnityEngine;

namespace FSM
{
    public abstract class FSMAction : ScriptableObject
    {
        public abstract void Execute(BaseStateMachine stateMachine);

        public abstract void OnEnter(BaseStateMachine stateMachine);

        public abstract void OnExit(BaseStateMachine stateMachine);
    }
}