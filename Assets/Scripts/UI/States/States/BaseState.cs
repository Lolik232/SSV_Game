using UnityEngine;

namespace FSM
{
	public class BaseState : ScriptableObject
	{
		public virtual void Execute(BaseStateMachine machine)
		{
		}
	}
}