using System;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatesManager", menuName = "Player/States/States Manager")]
public class PlayerStatesManagerSO : StatesManagerSO
{
	[Header("Grouded")]
	public PlayerIdleStateSO idle;
	public PlayerMoveStateSO move;
	public PlayerCrouchIdleStateSO crouchIdle;
	public PlayerCrouchMoveStateSO crouchMove;
	public PlayerLandStateSO land;

	[Header("Touching Wall")]
	public PlayerWallGrabStateSO wallGrab;
	public PlayerWallSlideStateSO wallSlide;
	public PlayerWallClimbStateSO wallClimb;

	[Header("On Ledge")]
	public PlayerLedgeGrabStateSO ledgeGrab;
	public PlayerLedgeHoldStateSO ledgeHold;
	public PlayerLedgeClimbStateSO ledgeClimb;

	[Header("In Air")]
	public PlayerInAirStateSO inAir;

	private void OnEnable()
	{
		states = new List<PlayerStateSO>
			{
				idle,
				move,
				crouchIdle,
				crouchMove,
				land,

				wallGrab,
				wallSlide,
				wallClimb,

				ledgeGrab,
				ledgeHold,
				ledgeClimb,

				inAir
			};
	}
}
