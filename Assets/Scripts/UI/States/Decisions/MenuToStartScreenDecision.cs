using System.Collections;
using System.Collections.Generic;

using FSM;

using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Menu To Start Screen")]
public class MenuToStartScreenDecision : Decision
{
	public override bool Decide(BaseStateMachine stateMachine)
	{
		return stateMachine.UIInputSO.escPressed;
	}
}