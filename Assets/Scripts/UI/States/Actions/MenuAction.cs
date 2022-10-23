using System.Collections;
using System.Collections.Generic;

using FSM;

using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Menu Action")]
public class MenuAction : FSMAction
{
	public override void Execute(BaseStateMachine stateMachine)
	{
		stateMachine.textAnim.SetBool("isTextHidden", true);
		stateMachine.menuAnim.SetBool("isTextHidden", true);
	}
}