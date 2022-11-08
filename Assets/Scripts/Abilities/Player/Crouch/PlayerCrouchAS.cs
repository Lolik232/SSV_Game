using UnityEngine;

[RequireComponent(typeof(CeilChecker))]

public class PlayerCrouchAS : AbilityState<PlayerCrouchAbility>
{
	private CeilChecker _ceilChecker;

	protected override void Awake()
	{
		base.Awake();
		Checkers.Add(_ceilChecker = GetComponent<CeilChecker>());
	}

	private void Start()
	{
		bool StandCondition() => !_ceilChecker.TouchingCeiling && Ability.Player.Input.Move.y != -1;

		Transitions.Add(new(Ability.Stand, StandCondition));
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		Ability.Player.Crouch();
	}
}
