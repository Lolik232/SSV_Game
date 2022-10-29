using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoveBackwardAbility", menuName = "Abilities/Move/Backward/Player")]

public class PlayerMoveBackwardAbilitySO : MoveBackwardAbilitySO
{
	[HideInInspector] [NonSerialized] protected new PlayerSO entity;

	protected override void OnEnable()
	{
		entity = (PlayerSO)base.entity;

		base.OnEnable();
	}
}
