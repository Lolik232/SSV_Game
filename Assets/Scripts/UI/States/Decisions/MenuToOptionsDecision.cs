using System.Collections;
using System.Collections.Generic;

using FSM;

using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Menu To Options")]
public class MenuToOptionsDecision : Decision
{
	public override bool Decide(BaseStateMachine stateMachine)
	{
		return stateMachine.UIInputSO.optionsPressed;
	}
}