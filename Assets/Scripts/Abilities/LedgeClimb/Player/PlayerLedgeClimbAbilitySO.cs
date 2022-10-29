using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLedgeClimbAbility", menuName = "Abilities/Ledge Climb/Player")]

public class PlayerLedgeClimbAbilitySO : AbilitySO
{
	[HideInInspector] [NonSerialized] protected new PlayerSO entity;

	protected Movable movable;

	private int _positionHolder;
	private int _directionHolder;

	protected override void OnEnable()
	{
		entity = base.entity as PlayerSO;

		base.OnEnable();

		enterConditions.Add(() => entity.checkers.touchingWall &&
															!entity.checkers.touchingLedge &&
															(entity.controller.move.x == movable.FacingDirection ||
															 entity.controller.move.y == 1));

		enterActions.Add(() =>
		{
			entity.checkers.DetermineLedgePosition();
			_directionHolder = movable.HoldDirection(-entity.checkers.wallDirection);
			_positionHolder = movable.HoldPosition(entity.checkers.ledgeStartPosition);
		});

		exitActions.Add(() =>
		{
			movable.ReleaseDirection(_directionHolder);
			movable.ReleasePosition(_positionHolder);
			movable.TrySetPosition(entity.checkers.ledgeEndPosition);
		});
	}

	public override void Initialize(GameObject origin)
	{
		base.Initialize(origin);
		movable = origin.GetComponent<Movable>();
	}
}
