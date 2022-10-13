using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatesManager", menuName = "Player/States/States Manager")]
public class PlayerStatesManagerSO : ScriptableObject
{
	public List<PlayerStateSO> States
	{
		get; private set;
	}

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

	[Header("Free")]
	public PlayerLedgeGrabStateSO ledgeGrab;
	public PlayerLedgeHoldStateSO ledgeHold;
	public PlayerLedgeClimbStateSO ledgeClimb;
	public PlayerInAirStateSO inAir;

	private void OnEnable()
	{
		States = new List<PlayerStateSO>
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

	public void Initialize(Player player)
	{
		foreach (var state in States)
		{
			state.Initialize(player);
		}
	}
}
