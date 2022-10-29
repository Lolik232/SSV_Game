using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerGrabAbility", menuName = "Abilities/Grab/Player")]

public class PlayerGrabAbilitySO : AbilitySO
{
	[HideInInspector] [NonSerialized] protected new PlayerSO entity;

	protected override void OnEnable()
	{
		entity = (PlayerSO)base.entity;

		base.OnEnable();

		enterConditions.Add(() => entity.controller.move.y == 0 && entity.controller.grab);

		exitConditions.Add(() => entity.controller.move.y != 0 || !entity.controller.grab);

		enterActions.Add(() =>
		{
			entity.HoldPosition(entity.Position);
		});

		exitActions.Add(() =>
		{
			entity.ReleasePosition();
		});
	}
}
