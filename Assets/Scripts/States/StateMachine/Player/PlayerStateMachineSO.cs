using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStateMachine", menuName = "States/Machine/Player")]

public class PlayerStateMachineSO : StateMachineSO
{
	public PlayerOnLedgeStateSO onLedge;

	protected override void OnEnable()
	{
		base.OnEnable();

		elements.Add(onLedge);
	}
}
