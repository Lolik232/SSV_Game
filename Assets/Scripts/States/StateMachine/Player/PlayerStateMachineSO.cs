using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStateMachine", menuName = "States/Machine/Player")]

public class PlayerStateMachineSO : StateMachineSO
{
	[HideInInspector] public new PlayerGroundedStateSO grounded;
	[HideInInspector] public new PlayerInAirStateSO inAir;

	public PlayerOnLedgeStateSO onLedge;
	public PlayerTouchingWallStateSO touchingWall;

	protected override void OnEnable()
	{
		grounded = (PlayerGroundedStateSO)base.grounded;
		inAir = (PlayerInAirStateSO)base.inAir;

		base.OnEnable();

		elements.Add(touchingWall);
		elements.Add(onLedge);
	}
}
