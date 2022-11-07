using UnityEngine;

[RequireComponent(typeof(Crouchable), typeof(MoveController), typeof(CeilChecker))]

public class PlayerCrouchAS : AbilityState<PlayerCrouchAbility>
{
	private Crouchable _crouchable;
	private CeilChecker _ceilChecker;
	private MoveController _moveController;

	protected override void Awake()
	{
		base.Awake();
		_crouchable = GetComponent<Crouchable>();
		_ceilChecker = GetComponent<CeilChecker>();
		_moveController = GetComponent<MoveController>();
	}

	private void Start()
	{
		bool StandCondition() => !_ceilChecker.TouchingCeiling && _moveController.Move.y != -1;

		Transitions.Add(new(Ability.Stand, StandCondition));
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		_crouchable.Crouch();
	}
}
