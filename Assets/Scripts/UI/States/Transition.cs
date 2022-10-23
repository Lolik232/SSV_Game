using UnityEngine;

namespace FSM
{
	[CreateAssetMenu(menuName = "FSM/Transition")]
	public sealed class Transition : ScriptableObject
	{
		public Decision Decision;
		public BaseState TrueState;
		public BaseState FalseState;

		public void Execute(BaseStateMachine stateMachine)
		{
			if (Decision.Decide(stateMachine) && TrueState is not RemainInState)
				stateMachine.CurrentState = TrueState;
			else if (FalseState is not RemainInState)
				stateMachine.CurrentState = FalseState;
		}
	}
}