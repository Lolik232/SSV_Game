using System.Collections;
using System.Collections.Generic;
using System.Data;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoveBackwardAbility", menuName = "Abilities/MoveBackward/Player")]

public class PlayerMoveBackwardAbilitySO : MoveBackwardAbilitySO
{
	[HideInInspector] protected new PlayerSO entity;

	protected override void OnEnable()
	{
		entity = (PlayerSO)base.entity;

		base.OnEnable();
	}
}
