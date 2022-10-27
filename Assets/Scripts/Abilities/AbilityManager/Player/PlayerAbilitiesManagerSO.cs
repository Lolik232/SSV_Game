using System;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAbilitiesManager", menuName = "Player/Abilities/Abilities Manager")]

public class PlayerAbilitiesManagerSO : AbilitiesManagerSO
{
	public AttackAbilitySO attack;
	public PlayerDashAbilitySO dash;
	public PlayerJumpAbilitySO jump;
	public PlayerWallJumpAbilitySO wallJump;

	protected override void OnEnable()
	{
		base.OnEnable();

		elements.Add(attack);
		elements.Add(dash);
		elements.Add(jump);
		elements.Add(wallJump);
	}
}