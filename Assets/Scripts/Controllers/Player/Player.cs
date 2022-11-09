using UnityEngine;

[RequireComponent(typeof(Physical), typeof(Movable), typeof(Crouchable))]
[RequireComponent(typeof(Rotateable))]

[RequireComponent(typeof(PlayerInputReader))]

[RequireComponent(typeof(PlayerGroundedState), typeof(PlayerInAirState), typeof(PlayerTouchingWallState))]
[RequireComponent(typeof(PlayerOnLedgeState))]

[RequireComponent(typeof(PlayerMoveHorizontalAbility), typeof(PlayerMoveVerticalAbility))]
[RequireComponent(typeof(PlayerLedgeClimbAbility))]
[RequireComponent(typeof(PlayerCrouchAbility))]
[RequireComponent(typeof(PlayerJumpAbility))]
[RequireComponent(typeof(PlayerDashAbility))]

public class Player : MonoBehaviour, IPhysical, IMovable, ICrouchable, IRotateable
{
	private Physical _physical;
	private Movable _movable;
	private Crouchable _crouchable;
	private Rotateable _rotateable;

	public PlayerInputReader Input
	{
		get;
		private set;
	}

	public PlayerGroundedState GroundedState
	{
		get;
		private set;
	}
	public PlayerInAirState InAirState
	{
		get;
		private set;
	}
	public PlayerTouchingWallState TouchingWallState
	{
		get;
		private set;
	}
	public PlayerOnLedgeState OnLedgeState
	{
		get;
		private set;
	}

	public PlayerMoveHorizontalAbility MoveHorizontalAbility
	{
		get;
		private set;
	}
	public PlayerMoveVerticalAbility MoveVerticalAbility
	{
		get;
		private set;
	}
	public PlayerJumpAbility JumpAbility
	{
		get;
		private set;
	}
	public PlayerCrouchAbility CrouchAbility
	{
		get;
		private set;
	}
	public PlayerLedgeClimbAbility LedgeClimbAbility
	{
		get;
		private set;
	}

	public PlayerDashAbility DashAbility
	{
		get;
		private set;
	}

	public Vector2 Position => _physical.Position;

	public Vector2 Velocity => _physical.Velocity;

	public float Gravity => _physical.Gravity;

	public Vector2 Size => _physical.Size;

	public Vector2 Offset => _physical.Offset;

	public Vector2 Center => _physical.Center;

	public bool IsPositionLocked => _movable.IsPositionLocked;

	public bool IsVelocityLocked => _movable.IsVelocityLocked;

	public Vector2 StandSize => _crouchable.StandSize;

	public Vector2 StandOffset => _crouchable.StandOffset;

	public Vector2 StandCenter => _crouchable.StandCenter;

	public Vector2 CrouchSize => _crouchable.CrouchSize;

	public Vector2 CrouchOffset => _crouchable.CrouchOffset;

	public Vector2 CrouchCenter => _crouchable.CrouchCenter;

	public bool IsStanding => _crouchable.IsStanding;

	public int FacingDirection => _rotateable.FacingDirection;

	public int BodyDirection => _rotateable.BodyDirection;

	public void BlockPosition()
	{
		_movable.BlockPosition();
	}

	public void BlockVelocity()
	{
		_movable.BlockVelocity();
	}

	public void Push(float force, Vector2 angle)
	{
		_physical.Push(force, angle);
	}

	public void ResetGravity()
	{
		_movable.ResetGravity();
	}

	public void RotateBodyIntoDirection(int direction)
	{
		_rotateable.RotateBodyIntoDirection(direction);
	}

	public void RotateIntoDirection(int direction)
	{
		_rotateable.RotateIntoDirection(direction);
	}

	public void SetGravity(float gravity)
	{
		_movable.SetGravity(gravity);
	}

	public void SetPosition(Vector2 position)
	{
		_movable.SetPosition(position);
	}

	public void SetPosition(float x, float y)
	{
		_movable.SetPosition(x, y);
	}

	public void SetpositionX(float x)
	{
		_movable.SetpositionX(x);
	}

	public void SetPositionY(float y)
	{
		_movable.SetPositionY(y);
	}

	public void SetVelocity(Vector2 velocity)
	{
		_movable.SetVelocity(velocity);
	}

	public void SetVelocity(float x, float y)
	{
		_movable.SetVelocity(x, y);
	}

	public void SetVelocity(float speed, Vector2 angle, int direction)
	{
		_movable.SetVelocity(speed, angle, direction);
	}

	public void SetVelocityX(float x)
	{
		_movable.SetVelocityX(x);
	}

	public void SetVelocityY(float Y)
	{
		_movable.SetVelocityY(Y);
	}

	public void Stand()
	{
		_crouchable.Stand();
	}

	public void Crouch()
	{
		_crouchable.Crouch();
	}

	public void UnlockPosition()
	{
		_movable.UnlockPosition();
	}

	public void UnlockVelocity()
	{
		_movable.UnlockVelocity();
	}

	private void Awake()
	{
		_physical = GetComponent<Physical>();
		_movable = GetComponent<Movable>();
		_rotateable = GetComponent<Rotateable>();
		_crouchable = GetComponent<Crouchable>();

		Input = GetComponent<PlayerInputReader>();

		GroundedState = GetComponent<PlayerGroundedState>();
		InAirState = GetComponent<PlayerInAirState>();
		TouchingWallState = GetComponent<PlayerTouchingWallState>();
		OnLedgeState = GetComponent<PlayerOnLedgeState>();

		MoveHorizontalAbility = GetComponent<PlayerMoveHorizontalAbility>();
		MoveVerticalAbility = GetComponent<PlayerMoveVerticalAbility>();
		JumpAbility = GetComponent<PlayerJumpAbility>();
		CrouchAbility = GetComponent<PlayerCrouchAbility>();
		LedgeClimbAbility = GetComponent<PlayerLedgeClimbAbility>();
		DashAbility = GetComponent<PlayerDashAbility>();
	}

	private void Start()
	{
		RotateIntoDirection(1);
		Stand();
	}
}
