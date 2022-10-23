using System.Collections;
using System.Collections.Generic;

using FSM;

using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Start Screen Action")]
public class StartScreenAction : FSMAction
{
	public override void Execute(BaseStateMachine stateMachine)
	{
		stateMachine.menuAnim.SetBool("isTextHidden", false);
		stateMachine.textAnim.SetBool("isTextHidden", false);
	}
}
