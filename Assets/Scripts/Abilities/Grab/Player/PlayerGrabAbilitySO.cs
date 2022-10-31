using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerGrabAbility", menuName = "Abilities/Grab/Player")]

public class PlayerGrabAbilitySO : AbilitySO
{
	protected Movable movable;

	private int _positionHolder;

	protected override void OnEnable()
	{
		base.OnEnable();

		//enterConditions.Add(() => entity.controller.move.y == 0 && entity.controller.grab);

		//exitConditions.Add(() => entity.controller.move.y != 0 || !entity.controller.grab);

		enterActions.Add(() =>
		{
			_positionHolder = movable.HoldPosition(movable.Position);
		});

		exitActions.Add(() =>
		{
			movable.ReleasePosition(_positionHolder);
		});
	}

	public override void Initialize(GameObject origin)
	{
		base.Initialize(origin);
		movable = origin.GetComponent<Movable>();
	}
}
