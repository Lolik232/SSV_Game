using UnityEngine;

[RequireComponent(typeof(Crouchable), typeof(MoveController))]

public class PlayerStandAS : AbilityState<PlayerCrouchAbility>
{
	private Crouchable _crouchable;
	private MoveController _moveController;

	protected override void Awake()
	{
		base.Awake();
		_crouchable = GetComponent<Crouchable>();
		_moveController = GetComponent<MoveController>();
	}

	private void Start()
	{
		bool CrouchCondition() => _moveController.Move.y == -1;

		Transitions.Add(new(Ability.Crouch, CrouchCondition));
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		_crouchable.Stand();
	}
}
