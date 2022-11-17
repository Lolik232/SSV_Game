using UnityEngine;

namespace FSM
{
    public class BaseState : ScriptableObject
    {
        public virtual void Execute(BaseStateMachine machine)
        {
        }

        public virtual void OnEnter(BaseStateMachine machine)
        {
        }

        public virtual void OnExit(BaseStateMachine machine)
        {
        }
    }
}