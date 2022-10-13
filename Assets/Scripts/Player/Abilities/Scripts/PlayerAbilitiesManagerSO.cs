using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAbilitiesManager", menuName = "Player/Abilities/Abilities Manager")]

public class PlayerAbilitiesManagerSO : ScriptableObject
{
	public List<PlayerAbilitySO> Abilities
	{
		get; private set;
	}

	public PlayerAttackAbilitySO attack;
	public PlayerDashAbilitySO dash;
	public PlayerJumpAbilitySO jump;
	public PlayerWallJumpAbilitySO wallJump;

	private void OnEnable()
	{
		Abilities = new List<PlayerAbilitySO>
		{
				attack,
				dash,
				jump,
				wallJump
		};
	}

	public void Initialize(Player player)
	{
		foreach (var ability in Abilities)
		{
			ability.Initialize(player);
		}
	}
}