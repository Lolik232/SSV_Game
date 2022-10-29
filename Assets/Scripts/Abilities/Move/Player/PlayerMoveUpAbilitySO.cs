using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoveUpAbility", menuName = "Abilities/Move/Up/Player")]

public class PlayerMoveUpAbilitySO : MoveUpAbilitySO
{
	[HideInInspector] [NonSerialized] protected new PlayerSO entity;

	protected override void OnEnable()
	{
		entity = (PlayerSO)base.entity;

		base.OnEnable();
	}
}
