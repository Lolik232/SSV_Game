using UnityEngine;

[RequireComponent(typeof(PlayerJumpAS), typeof(Movable), typeof(JumpController))]

public class PlayerJumpAbility : Ability
{
	private JumpController _jumpController;
	private Movable _movable;

	public PlayerJumpAS Jump
	{
		get;
		private set;
	}

	protected override void Awake()
	{
		base.Awake();
		_jumpController = GetComponent<JumpController>();
		_movable = GetComponent<Movable>();

		Default = Jump = GetComponent<PlayerJumpAS>();

		GetAbilityStates<PlayerJumpAbility>();
	}

	private void Start()
	{
		enterConditions.Add(() => _jumpController.Jump && !_movable.IsVelocityLocked && !_movable.IsPositionLocked);
		exitConditions.Add(() => false);
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		_jumpController.Jump = false;
	}
}
