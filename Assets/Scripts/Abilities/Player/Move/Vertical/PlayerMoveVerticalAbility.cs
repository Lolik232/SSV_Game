using UnityEngine;

[RequireComponent(typeof(PlayerWallGrabAS), typeof(PlayerWallClimbAS), typeof(PlayerWallSlideAS))]
[RequireComponent(typeof(Movable), typeof(GrabController))]
[RequireComponent(typeof(MoveController), typeof(Rotateable))]

public class PlayerMoveVerticalAbility : Ability
{
	private Movable _movable;
	private Rotateable _rotateable;
	private GrabController _grabController;
	private MoveController _moveController;

	public PlayerWallSlideAS Slide
	{
		get;
		private set;
	}
	public PlayerWallClimbAS Climb
	{
		get;
		private set;
	}
	public PlayerWallGrabAS Grab
	{
		get;
		private set;
	}

	protected override void Awake()
	{
		base.Awake();
		_movable = GetComponent<Movable>();
		_rotateable = GetComponent<Rotateable>();
		_grabController = GetComponent<GrabController>();
		_moveController = GetComponent<MoveController>();

		Default = Grab = GetComponent<PlayerWallGrabAS>();
		Climb = GetComponent<PlayerWallClimbAS>();
		Slide = GetComponent<PlayerWallSlideAS>();


		GetAbilityStates<PlayerMoveVerticalAbility>();

		IsContinuous = true;
	}

	private void Start()
	{
		bool StayCondition() => !_movable.IsVelocityLocked && !_movable.IsPositionLocked &&
															(_grabController.Grab || _moveController.Move.x == _rotateable.FacingDirection);

		enterConditions.Add(() => StayCondition());
		exitConditions.Add(() => !StayCondition());
	}
}
