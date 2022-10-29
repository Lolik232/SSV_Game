using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttackAbility", menuName = "Abilities/Attack/Player")]

public class PlayerAttackAbilitySO : AbilitySO
{
	[HideInInspector] [NonSerialized] protected new PlayerSO entity;

	protected override void OnEnable()
	{
		entity = (PlayerSO)base.entity;

		base.OnEnable();

		enterConditions.Add(() => entity.controller.attack);

		enterActions.Add(() =>
		{
			entity.controller.attack = false;
			entity.weapons.Current.OnEnter();
		});

		exitActions.Add(() =>
		{
			entity.weapons.Current.OnExit();
		});
	}

	public void HoldDirection(int direction)
	{
		entity.weapons.Current.HoldDirection(direction);
	}

	public void ReleaseDirection()
	{
		entity.weapons.Current.ReleaseDirection();
	}
}
