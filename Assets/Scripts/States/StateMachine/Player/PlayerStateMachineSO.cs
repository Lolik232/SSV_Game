using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStateMachine", menuName = "States/Machine/Player")]

public class PlayerStateMachineSO : StateMachineSO
{
	[HideInInspector] [NonSerialized] public new PlayerGroundedStateSO grounded;
	[HideInInspector] [NonSerialized] public new PlayerInAirStateSO inAir;

	public PlayerTouchingWallStateSO touchingWall;

	protected override void OnEnable()
	{
		grounded = base.grounded as PlayerGroundedStateSO;
		inAir = base.inAir as PlayerInAirStateSO;

		base.OnEnable();

		elements.Add(touchingWall);
	}
}
