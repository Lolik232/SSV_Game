using System;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(PlayerInputReaderOwner), typeof(PlayerAbilitiesManagerOwner), typeof(PlayerParametersManagerOwner))]
[RequireComponent(typeof(StateMachine), typeof(Rigidbody2D), typeof(Animator))]

public class Player : MonoBehaviour
{
	private const float CHECK_OFFSET = 0.02f;
	private const float UNIT_SIZE = 1f;

	//TODO: удалить _weaponBase нахуй
	[SerializeField] private GameObject _weaponBase;

	[SerializeField] private float _groundCheckDistance;
	[SerializeField] private float _wallCheckDistance;
	[SerializeField] private float _ledgeCheckDistance;
	[SerializeField] private float _groundCloseCheckDistance;
	[SerializeField] private float _wallCloseCheckDistance;

	[SerializeField] private float _wallCheckerHeight;
	[SerializeField] private float _ledgeCheckerHeight;

	[SerializeField] private List<LayerMask> _whatIsTarget;

	[SerializeField] private Vector2 _standOffset;
	[SerializeField] private Vector2 _standSize;
	[SerializeField] private Vector2 _crouchOffset;
	[SerializeField] private Vector2 _crouchSize;

	[NonSerialized] public int facingDirection = 1;
	[NonSerialized] public int wallDirection;

	[NonSerialized] public bool isStanding;

	[NonSerialized] public bool isGrounded;
	[NonSerialized] public bool isGroundClose;
	[NonSerialized] public bool isTouchingCeiling;
	[NonSerialized] public bool isTouchingCeilingWhenClimb;
	[NonSerialized] public bool isTouchingWall;
	[NonSerialized] public bool isTouchingWallBack;
	[NonSerialized] public bool isClampedBetweenWalls;
	[NonSerialized] public bool isTouchingLedge;

	[NonSerialized] public Vector2 wallPosition;
	[NonSerialized] public Vector2 ledgeStartPosition;
	[NonSerialized] public Vector2 ledgeEndPosition;

	[NonSerialized] public Weapon weapon;
	[NonSerialized] public BoxCollider2D col;
	[NonSerialized] public Rigidbody2D rb;
	[NonSerialized] public StateMachine machine;
	[NonSerialized] public Animator anim;
	[NonSerialized] public TrailRenderer tr;

	private readonly Blocker _positionBlocker = new();
	private readonly Blocker _velocityBlocker = new();
	private readonly Blocker _gravityBlocker = new();
	private readonly Blocker _directionBlocker = new();
	private Vector2 _holdPosition;
	private Vector2 _cachedVelocity;
	private float _cachedGravity;

	private Vector2 _cornerPosition;

	public Vector2 Position => transform.position;

	public Vector2 Size => col.size;

	public Vector2 StandSize => _standSize;

	public Vector2 CrouchSize => _crouchSize;

	public Vector2 Center => Position + col.offset;

	public Vector2 StandCenter => Position + _standOffset;

	public Vector2 CrouchCenter => Position + _crouchOffset;

	public Vector2 CheckerOffset => new(Size.x / 2 - CHECK_OFFSET, Size.y / 2 - CHECK_OFFSET);

	public Vector2 StartLedgeOffset => new(Size.x / 2 + CHECK_OFFSET, UNIT_SIZE * _ledgeCheckerHeight + CHECK_OFFSET);

	public Vector2 EndLedgeOffset => new(Size.x / 2 + CHECK_OFFSET, CHECK_OFFSET);

	private Vector2 GroundCheckerPosition => new(Center.x, Center.y - CheckerOffset.y);

	private Vector2 WallCheckerPosition => new(Center.x + facingDirection * CheckerOffset.x, Position.y + UNIT_SIZE * _wallCheckerHeight);

	private Vector2 WallBackCheckerPosition => new(Center.x - facingDirection * CheckerOffset.x, Position.y + UNIT_SIZE * _wallCheckerHeight);

	private Vector2 CeilingCheckerPosition => new(Center.x, Center.y + CheckerOffset.y);

	private Vector2 LedgeCheckerPosition => new(Center.x + facingDirection * CheckerOffset.x, Position.y + UNIT_SIZE * _ledgeCheckerHeight);

	private Vector2 CornerCheckerPosition => new(LedgeCheckerPosition.x + facingDirection * _ledgeCheckDistance, LedgeCheckerPosition.y);

	private void Awake()
	{
		tr = GetComponent<TrailRenderer>();
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<BoxCollider2D>();
		machine = GetComponent<StateMachine>();
		anim = GetComponent<Animator>();
		weapon = _weaponBase.GetComponent<Weapon>();
	}

	private void Start()
	{
		Stand();
	}

	private void Update()
	{
		if (_positionBlocker.IsLocked)
		{
			transform.position = _holdPosition;
		}
	}

	private void CheckIfGrounded()
	{
		foreach (var target in _whatIsTarget)
		{
			isGrounded = Physics2D.OverlapBox(GroundCheckerPosition, new Vector2(CheckerOffset.x * 2, _groundCheckDistance), 0, target);
			if (isGrounded)
			{
				return;
			}
		}
	}

	private void CheckIfGroundClose()
	{
		foreach (var target in _whatIsTarget)
		{
			isGroundClose = Physics2D.Raycast(GroundCheckerPosition, Vector2.down, _groundCheckDistance, target);
			if (isGroundClose)
			{
				return;
			}
		}
	}

	private void CheckIfTouchingCeiling()
	{
		foreach (var target in _whatIsTarget)
		{
			isTouchingCeiling = Physics2D.OverlapBox(CeilingCheckerPosition, new Vector2(CheckerOffset.x * 2, _groundCheckDistance), 0, target);
			if (isTouchingCeiling)
			{
				return;
			}
		}
	}

	private void CheckIfTouchingWall()
	{
		foreach (var target in _whatIsTarget)
		{
			RaycastHit2D hit = Physics2D.Raycast(WallCheckerPosition, facingDirection * Vector2.right, _wallCheckDistance, target);
			wallDirection = hit ? -facingDirection : facingDirection;
			isTouchingWall = hit;
			if (isTouchingWall)
			{
				wallPosition.Set(WallCheckerPosition.x + facingDirection * hit.distance, WallCheckerPosition.y);
				return;
			}
		}
	}

	private void CheckIfTouchingWallBack()
	{
		foreach (var target in _whatIsTarget)
		{
			isTouchingWallBack = Physics2D.Raycast(WallBackCheckerPosition, facingDirection * Vector2.left, _wallCheckDistance, target);
			if (isTouchingWallBack)
			{
				return;
			}
		}
	}

	private void CheckIfClampedBetweenWalls()
	{
		foreach (var target in _whatIsTarget)
		{
			isClampedBetweenWalls = Physics2D.Raycast(WallBackCheckerPosition, facingDirection * Vector2.left, _wallCloseCheckDistance, target) &&
															Physics2D.Raycast(WallCheckerPosition, facingDirection * Vector2.right, _wallCloseCheckDistance, target) ||
															Physics2D.Raycast(new Vector2(WallBackCheckerPosition.x, LedgeCheckerPosition.y), facingDirection * Vector2.left, _wallCloseCheckDistance, target) &&
															Physics2D.Raycast(new Vector2(WallCheckerPosition.x, LedgeCheckerPosition.y), facingDirection * Vector2.right, _wallCloseCheckDistance, target);
			if (isClampedBetweenWalls)
			{
				return;
			}
		}
	}

	private void CheckIfTouchingLedge()
	{
		foreach (var target in _whatIsTarget)
		{
			isTouchingLedge = Physics2D.Raycast(LedgeCheckerPosition, facingDirection * Vector2.right, _ledgeCheckDistance, target);
			if (isTouchingLedge)
			{
				return;
			}
		}
	}

	private void CheckIfTouchingCeilingWhenClimb()
	{
		foreach (var target in _whatIsTarget)
		{
			isTouchingCeilingWhenClimb = Physics2D.Raycast(new Vector2(ledgeEndPosition.x, ledgeEndPosition.y + UNIT_SIZE - _groundCheckDistance / 2), Vector2.up, _groundCheckDistance, target);
			if (isTouchingCeilingWhenClimb)
			{
				return;
			}
		}
	}

	public void CheckIfShouldFlip(int direction)
	{
		if (facingDirection != direction && direction != 0 && !_directionBlocker.IsLocked)
		{
			facingDirection = -facingDirection;
			transform.Rotate(0f, 180f, 0f);
		}
	}

	private void DetermineCornerPosition()
	{
		foreach (var target in _whatIsTarget)
		{
			RaycastHit2D hit = Physics2D.Raycast(CornerCheckerPosition, Vector2.down, LedgeCheckerPosition.y - WallBackCheckerPosition.y + CHECK_OFFSET, target);
			if (hit)
			{
				_cornerPosition.Set(wallPosition.x, LedgeCheckerPosition.y - hit.distance);
				return;
			}
		}
	}

	public void DetermineLedgePosition()
	{
		ledgeStartPosition.Set(_cornerPosition.x + wallDirection * StartLedgeOffset.x,
													 _cornerPosition.y - StartLedgeOffset.y);
		ledgeEndPosition.Set(_cornerPosition.x - wallDirection * EndLedgeOffset.x,
												 _cornerPosition.y + EndLedgeOffset.y);
	}

	public void DoChecks()
	{
		CheckIfGrounded();
		CheckIfGroundClose();
		CheckIfTouchingLedge();
		CheckIfTouchingWall();
		CheckIfTouchingWallBack();
		CheckIfTouchingCeiling();
		CheckIfTouchingCeilingWhenClimb();
		CheckIfClampedBetweenWalls();

		if (!isTouchingLedge && isTouchingWall)
		{
			DetermineCornerPosition();
		}
	}

	public void TrySetVelocity(float velocity, Vector2 angle, int direction)
	{
		TrySetVelocity(new Vector2(angle.normalized.x * velocity * direction, angle.normalized.y * velocity));
	}

	public void TrySetVelocityX(float xVelocity)
	{
		TrySetVelocity(new Vector2(xVelocity, rb.velocity.y));
	}

	public void TrySetVelocityY(float yVelocity)
	{
		TrySetVelocity(new Vector2(rb.velocity.x, yVelocity));
	}

	public void TrySetVelocityZero()
	{
		TrySetVelocity(Vector2.zero);
	}

	public void TrySetVelocity(Vector2 velocity)
	{
		if (!_velocityBlocker.IsLocked)
		{
			rb.velocity = velocity;
		}
		else
		{
			CacheVelocity(velocity);
		}
	}

	public void TrySetGravity(float gravity)
	{
		if (!_gravityBlocker.IsLocked)
		{
			rb.gravityScale = gravity;
		}
		else
		{
			CacheGravity(gravity);
		}
	}

	public void HoldVelocity(float velocity, Vector2 angle, int direction)
	{
		HoldVelocity(new Vector2(angle.normalized.x * velocity * direction, angle.normalized.y * velocity));
	}

	public void HoldVelocity(Vector2 velocity)
	{
		if (!_velocityBlocker.IsLocked)
		{
			CacheVelocity(rb.velocity);
		}

		rb.velocity = velocity;
		_velocityBlocker.AddBlock();
	}

	public void ReleaseVelocity()
	{
		_velocityBlocker.RemoveBlock();
		if (!_velocityBlocker.IsLocked)
		{
			rb.velocity = _cachedVelocity;
		}
	}

	public void CacheVelocity(Vector2 velocity)
	{
		_cachedVelocity = velocity;
	}

	public void HoldGravity(float gravity)
	{
		if (!_gravityBlocker.IsLocked)
		{
			CacheGravity(rb.gravityScale);
		}

		rb.gravityScale = gravity;
		_gravityBlocker.AddBlock();
	}

	public void ReleaseGravity()
	{
		_gravityBlocker.RemoveBlock();
		if (!_gravityBlocker.IsLocked)
		{
			rb.gravityScale = _cachedGravity;
		}
	}

	public void CacheGravity(float gravity)
	{
		_cachedGravity = gravity;
	}

	public void HoldDirection(int direction)
	{
		CheckIfShouldFlip(direction);
		_directionBlocker.AddBlock();
	}

	public void ReleaseDirection()
	{
		_directionBlocker.RemoveBlock();
	}

	public void HoldPosition(Vector2 holdPosition)
	{
		if (!_positionBlocker.IsLocked)
		{
			HoldGravity(0f);
			HoldVelocity(Vector2.zero);
			CacheVelocity(Vector2.zero);
		}

		_holdPosition = holdPosition;
		transform.position = holdPosition;
		_positionBlocker.AddBlock();
	}

	public void ReleasePosition()
	{
		_positionBlocker.RemoveBlock();
		if (!_positionBlocker.IsLocked)
		{
			ReleaseGravity();
			ReleaseVelocity();
		}
	}

	public void MoveToX(float x)
	{
		transform.position = new Vector2(x, transform.position.y);
	}

	public void MoveToY(float y)
	{
		transform.position = new Vector2(transform.position.x, y);
	}

	public void Stand()
	{
		isStanding = true;
		col.size = _standSize;
		col.offset = _standOffset;
	}

	public void Crouch()
	{
		isStanding = false;
		col.size = _crouchSize;
		col.offset = _crouchOffset;
	}

	private void OnDrawGizmos()
	{
		if (col != null)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y + col.offset.y), col.size);

			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(new Vector2(ledgeEndPosition.x, ledgeEndPosition.y + col.offset.y), col.size);
			Gizmos.DrawWireCube(new Vector2(ledgeStartPosition.x, ledgeStartPosition.y + col.offset.y), col.size);

			Gizmos.color = Color.red;
			Gizmos.DrawLine(new Vector2(ledgeEndPosition.x, ledgeEndPosition.y + UNIT_SIZE - _groundCheckDistance / 2),
											new Vector2(ledgeEndPosition.x, ledgeEndPosition.y + UNIT_SIZE + _groundCheckDistance / 2));

			Gizmos.color = Color.white;
			Gizmos.DrawLine(new Vector2(WallBackCheckerPosition.x - facingDirection * _wallCloseCheckDistance, WallBackCheckerPosition.y),
											new Vector2(WallCheckerPosition.x + facingDirection * _wallCloseCheckDistance, WallCheckerPosition.y));
			Gizmos.DrawLine(new Vector2(WallBackCheckerPosition.x - facingDirection * _wallCloseCheckDistance, LedgeCheckerPosition.y),
											new Vector2(WallCheckerPosition.x + facingDirection * _wallCloseCheckDistance, LedgeCheckerPosition.y));
			Gizmos.DrawLine(GroundCheckerPosition, new Vector2(GroundCheckerPosition.x, GroundCheckerPosition.y - _groundCloseCheckDistance));

			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(GroundCheckerPosition, new Vector2(CheckerOffset.x * 2, _groundCheckDistance * 2));
			Gizmos.DrawWireCube(CeilingCheckerPosition, new Vector2(CheckerOffset.x * 2, _groundCheckDistance * 2));
			Gizmos.DrawLine(new Vector2(WallBackCheckerPosition.x - facingDirection * _wallCheckDistance, WallBackCheckerPosition.y), WallBackCheckerPosition);
			Gizmos.DrawLine(WallCheckerPosition, new Vector2(WallCheckerPosition.x + facingDirection * _wallCheckDistance, WallCheckerPosition.y));
			Gizmos.DrawLine(LedgeCheckerPosition, new Vector2(LedgeCheckerPosition.x + facingDirection * _ledgeCheckDistance, LedgeCheckerPosition.y));
			Gizmos.DrawLine(CornerCheckerPosition, new Vector2(CornerCheckerPosition.x, WallCheckerPosition.y - CHECK_OFFSET));

			Gizmos.color = Color.cyan;
			Gizmos.DrawWireSphere(Center, 1);
			Gizmos.DrawLine(Center - Vector2.one.normalized, Center + Vector2.one.normalized);
			Gizmos.DrawLine(Center - new Vector2(-1f, 1f).normalized, Center + new Vector2(-1f, 1f).normalized);
		}
	}
}
