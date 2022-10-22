using System;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAbilitiesManager", menuName = "Player/Abilities/Abilities Manager")]

public class PlayerAbilitiesManagerSO : ScriptableObject
{
	[NonSerialized] public List<PlayerAbilitySO> abilities;

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

	public void Initialize(Player player, Animator anim)
	{
		foreach (var ability in abilities)
		{
			ability.Initialize(player, anim);
		}
	}
}