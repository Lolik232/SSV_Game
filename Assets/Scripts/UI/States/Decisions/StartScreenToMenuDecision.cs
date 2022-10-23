using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using FSM;

using Unity.VisualScripting;

using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "FSM/Decisions/Start Screen To Menu")]
public class StartScreenToMenuDecision : Decision
{
	public override bool Decide(BaseStateMachine stateMachine)
	{
		return stateMachine.UIInput.enterPressed;


	}
}