using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoveForwardAbility", menuName = "Abilities/Move/Forward/Player")]

public class PlayerMoveForwardAbilitySO : MoveForwardAbilitySO
{
	[HideInInspector] protected new PlayerSO entity;

	protected override void OnEnable()
	{
		entity = (PlayerSO)base.entity;

		base.OnEnable();
	}
}
