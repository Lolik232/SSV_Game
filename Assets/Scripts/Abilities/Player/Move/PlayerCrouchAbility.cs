using UnityEngine;

[RequireComponent(typeof(Crouchable), typeof(MoveController))]

public class PlayerCrouchAbility : Ability
{
	private Crouchable _crouchable;
	private MoveController _moveController;

	protected override void Awake()
	{
		base.Awake();
		_crouchable = GetComponent<Crouchable>();
		_moveController = GetComponent<MoveController>();

		enterConditions.Add(()=> !_crouchable.CanStand || _moveController.Move.y == -1);
		exitConditions.Add(()=> _crouchable.CanStand && _moveController.Move.y != -1);
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		_crouchable.Crouch();
	}

	protected override void ApplyExitActions()
	{
		base.ApplyEnterActions();
		_crouchable.Stand();
	}
}
