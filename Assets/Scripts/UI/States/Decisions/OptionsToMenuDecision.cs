using System.Collections;
using System.Collections.Generic;

using FSM;

using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Options To Menu Decision")]
public class OptionsToMenuDecision : Decision
{
	public override bool Decide(BaseStateMachine stateMachine)
	{
		return stateMachine.UIInputSO.escPressed && stateMachine.UIInputSO.optionsPressed;
	}
}