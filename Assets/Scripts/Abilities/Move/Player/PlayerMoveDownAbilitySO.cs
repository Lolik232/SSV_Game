using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoveDownAbility", menuName = "Abilities/Move/Down/Player")]

public class PlayerMoveDownAbilitySO : MoveDownAbilitySO
{
	[HideInInspector] protected new PlayerSO entity;

	protected override void OnEnable()
	{
		entity = (PlayerSO)base.entity;

		base.OnEnable();
	}
}
