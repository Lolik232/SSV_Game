using System;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAbilitiesManager", menuName = "Player/Abilities/Abilities Manager")]

public class PlayerAbilitiesManagerSO : AbilitiesManagerSO
{
	public PlayerAttackAbilitySO attack;
	public PlayerDashAbilitySO dash;
	public PlayerJumpAbilitySO jump;
	public PlayerWallJumpAbilitySO wallJump;

	private void OnEnable()
	{
		abilities = new List<PlayerAbilitySO>
		{
				attack,
				dash,
				jump,
				wallJump
		};
	}
}