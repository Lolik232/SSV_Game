using UnityEngine;

[RequireComponent(typeof(PlayerWallGrabAS), typeof(PlayerWallClimbAS), typeof(PlayerWallSlideAS))]
[RequireComponent(typeof(Movable), typeof(GrabController))]
[RequireComponent(typeof(MoveController), typeof(Rotateable))]

public class PlayerMoveVerticalAbility : Ability
{
	public Player Player
	{
		get;
		private set;
	}

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
		Player = GetComponent<Player>();

		Default = Grab = GetComponent<PlayerWallGrabAS>();
		Climb = GetComponent<PlayerWallClimbAS>();
		Slide = GetComponent<PlayerWallSlideAS>();

		GetAbilityStates<PlayerMoveVerticalAbility>();
	}

	private void Start()
	{
		bool StayCondition() => !Player.IsVelocityLocked && !Player.IsPositionLocked &&
															(Player.Input.Grab || Player.Input.Move.x == Player.FacingDirection);

		enterConditions.Add(() => StayCondition());
		exitConditions.Add(() => !StayCondition());
	}
}
