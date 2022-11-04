using UnityEngine;

[RequireComponent(typeof(MoveController))]

public class PlayerStandAbility : MoveStopAbility
{
	private MoveController _moveController;

	protected override void Awake()
	{
		base.Awake();
		_moveController = GetComponent<MoveController>();

		enterConditions.Add(() => _moveController.Move.x == 0);
		exitConditions.Add(() => _moveController.Move.x != 0);
	}

	protected override void ApplyPrepareActions()
	{
		base.ApplyPrepareActions();
		startSpeed = movable.Velocity.x;
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		movable.SetVelocityX(moveSpeed);
	}
}
