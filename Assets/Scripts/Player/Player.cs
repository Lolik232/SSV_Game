using System;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(PlayerInputReaderOwner))]
[RequireComponent(typeof(StateMachine), typeof(Rigidbody2D), typeof(Animator))]

public class Player : MonoBehaviour
{
	//TODO: удалить _weaponBase нахуй
	[SerializeField] private GameObject _weaponBase;

	[SerializeField] private PlayerStatesManagerSO _statesManager;
	[SerializeField] private PlayerAbilitiesManagerSO _abilitiesManager;

	[Header("Checkers")]
	[SerializeField] private Transform _groundChecker;
	[SerializeField] private Transform _ceilingChecker;
	[SerializeField] private Transform _wallChecker;
	[SerializeField] private Transform _ledgeChecker;

	[SerializeField] private float _groundCheckRadius;
	[SerializeField] private float _groundCheckDistance;
	[SerializeField] private float _wallCheckDistance;
	[SerializeField] private float _ledgeCheckDistance;
	[SerializeField] private float _wallCloseCheckDistance;

	[SerializeField] private List<LayerMask> _whatIsTarget;

	[Header("Parameters")]
	[SerializeField] private Vector2 _standOffset;
	[SerializeField] private Vector2 _standSize;

	[SerializeField] private Vector2 _crouchOffset;
	[SerializeField] private Vector2 _crouchSize;

	[Header("Movement")]
	[SerializeField] private int _moveSpeed;
	[SerializeField] private int _crouchMoveSpeed;
	[SerializeField] private int _inAirMoveSpeed;

	[Header("Touching Wall")]
	[SerializeField] private int _wallSlideSpeed;
	[SerializeField] private int _wallClimbSpeed;

	[Header("On Ledge")]
	[SerializeField] private Vector2 _startLedgeOffset;
	[SerializeField] private Vector2 _endLedgeOffset;
	[NonSerialized] public int facingDirection = 1;
	[NonSerialized] public int wallDirection;

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

	[NonSerialized] public PlayerInputReaderSO inputReader;
	[NonSerialized] public Weapon weapon;
	[NonSerialized] public BoxCollider2D col;
	[NonSerialized] public Rigidbody2D rb;
	[NonSerialized] public StateMachine sm;
	[NonSerialized] public Animator anim;
	[NonSerialized] public TrailRenderer tr;

	private bool _isVelocityHeldOn;
	private Vector2 _cachedVelocity;
	private Vector2 _cornerPosition;

	public int MoveSpeed => _moveSpeed;
	public int CrouchMoveSpeed => _crouchMoveSpeed;
	public int InAirMoveSpeed => _inAirMoveSpeed;
	public int WallSlideSpeed => _wallSlideSpeed;
	public int WallClimbSpeed => _wallClimbSpeed;
	public Vector2 StandSize => _standSize;
	public Vector2 CrouchSize => _crouchSize;
	public Vector2 Center => (Vector2)transform.position + col.offset;
	private Vector2 CornerCheckerPosition => new(_ledgeChecker.position.x + _ledgeCheckDistance * facingDirection, _ledgeChecker.position.y);


	private void Awake()
	{
		tr = GetComponent<TrailRenderer>();
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<BoxCollider2D>();
		sm = GetComponent<StateMachine>();
		anim = GetComponent<Animator>();
		inputReader = GetComponent<PlayerInputReaderOwner>().inputReader;
		weapon = _weaponBase.GetComponent<Weapon>();
	}

	private void Start()
	{
		Stand();
		facingDirection = 1;

		_statesManager.Initialize(this);
		_abilitiesManager.Initialize(this);
	}

	private void Update()
	{
		bool abilityUsed = false;
		foreach (var ability in _abilitiesManager.abilities)
		{
			if (!abilityUsed)
			{
				abilityUsed |= ability.TryUseAbility();
			}

			ability.OnUpdate();
		}
	}

	private void OnAttackEnd() => _abilitiesManager.attack.Terminate();

	public void CheckIfGrounded()
	{
		foreach (var target in _whatIsTarget)
		{
			isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, target);
			if (isGrounded)
			{
				return;
			}
		}
	}
	public void CheckIfGroundClose()
	{
		foreach (var target in _whatIsTarget)
		{
			isGroundClose = Physics2D.Raycast(_groundChecker.position, Vector2.down, _groundCheckDistance, target);
			if (isGroundClose)
			{
				return;
			}
		}
	}

	public void CheckIfTouchingCeiling()
	{
		foreach (var target in _whatIsTarget)
		{
			isTouchingCeiling = Physics2D.OverlapCircle(_ceilingChecker.position, _groundCheckRadius, target);
			if (isTouchingCeiling)
			{
				return;
			}
		}
	}
	public void CheckIfTouchingWall()
	{
		foreach (var target in _whatIsTarget)
		{
			RaycastHit2D hit = Physics2D.Raycast(_wallChecker.position, facingDirection * Vector2.right, _wallCheckDistance, target);
			wallDirection = hit ? -facingDirection : facingDirection;
			isTouchingWall = hit;
			if (isTouchingWall)
			{
				wallPosition.Set(_wallChecker.position.x + facingDirection * hit.distance, _wallChecker.position.y);
				return;
			}
		}
	}
	public void CheckIfTouchingWallBack()
	{
		foreach (var target in _whatIsTarget)
		{
			isTouchingWallBack = Physics2D.Raycast(_wallChecker.position, facingDirection * Vector2.left, _wallCheckDistance, target);
			if (isTouchingWallBack)
			{
				return;
			}
		}
	}

	public void CheckIfClampedBetweenWalls()
	{
		foreach (var target in _whatIsTarget)
		{
			isClampedBetweenWalls = Physics2D.Raycast(_wallChecker.position, Vector2.right, _wallCloseCheckDistance, target) &&
															Physics2D.Raycast(_wallChecker.position, Vector2.left, _wallCloseCheckDistance, target) ||
															Physics2D.Raycast(_ledgeChecker.position, Vector2.right, _wallCloseCheckDistance, target) &&
															Physics2D.Raycast(_ledgeChecker.position, Vector2.left, _wallCloseCheckDistance, target);
			if (isClampedBetweenWalls)
			{
				return;
			}
		}
	}

	public void CheckIfTouchingLedge()
	{
		foreach (var target in _whatIsTarget)
		{
			isTouchingLedge = Physics2D.Raycast(_ledgeChecker.position, facingDirection * Vector2.right, _ledgeCheckDistance, target);
			if (isTouchingLedge)
			{
				return;
			}
		}
	}

	public void CheckIfTouchingCeilingWhenClimb()
	{
		foreach (var target in _whatIsTarget)
		{
			isTouchingCeilingWhenClimb = Physics2D.Raycast(ledgeEndPosition, Vector2.up, _standSize.y, target);
			if (isTouchingCeilingWhenClimb)
			{
				return;
			}
		}
	}

	public void CheckIfShouldFlip(int direction)
	{
		if (facingDirection != direction && direction != 0)
		{
			facingDirection = -facingDirection;
			transform.Rotate(0f, 180f, 0f);
		}
	}

	public void DetermineCornerPosition()
	{
		foreach (var target in _whatIsTarget)
		{
			RaycastHit2D hit = Physics2D.Raycast(CornerCheckerPosition, Vector2.down, _ledgeChecker.position.y - _wallChecker.position.y + 0.02f, target);
			if (hit)
			{
				_cornerPosition.Set(wallPosition.x, _ledgeChecker.position.y - hit.distance);
				return;
			}
		}
	}

	public void DetermineLedgePosition()
	{
		ledgeStartPosition.Set(_cornerPosition.x + wallDirection * (_startLedgeOffset.x + 0.02f),
													 _cornerPosition.y - _startLedgeOffset.y - 0.02f);
		ledgeEndPosition.Set(_cornerPosition.x - wallDirection * _endLedgeOffset.x,
												 _cornerPosition.y + _endLedgeOffset.y + 0.02f);
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
		_cachedVelocity = velocity;
		if (!_isVelocityHeldOn)
		{
			rb.velocity = _cachedVelocity;
		}
	}

	public void HoldVelocity(float velocity, Vector2 angle, int direction)
	{
		HoldVelocity(new Vector2(angle.normalized.x * velocity * direction, angle.normalized.y * velocity));
	}

	public void HoldVelocityX(float xVelocity)
	{
		HoldVelocity(new Vector2(xVelocity, rb.velocity.y));
	}

	public void HoldVelocityY(float yVelocity)
	{
		HoldVelocity(new Vector2(rb.velocity.x, yVelocity));
	}

	public void HoldVelocity(Vector2 velocity)
	{
		rb.velocity = velocity;
		_isVelocityHeldOn = true;
	}

	public void ReleaseVelocity()
	{
		rb.velocity = _cachedVelocity;
		_isVelocityHeldOn = false;
	}

	public void MoveToX(float x)
	{
		transform.position = new Vector2(x, transform.position.y);
	}

	public void MoveToY(float y)
	{
		transform.position = new Vector2(transform.position.x, y);
	}

	public void HoldPosition(Vector2 holdPosition)
	{
		transform.position = holdPosition;
		rb.velocity = Vector2.zero;
	}

	public void Stand()
	{
		col.size = _standSize;
		col.offset = _standOffset;
	}

	public void Crouch()
	{
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
			Gizmos.DrawLine(ledgeEndPosition, new Vector2(ledgeEndPosition.x, ledgeEndPosition.y + _standSize.y));
		}

		Gizmos.color = Color.white;
		Gizmos.DrawLine(new Vector2(_wallChecker.position.x - _wallCloseCheckDistance, _wallChecker.position.y), new Vector2(_wallChecker.position.x + _wallCloseCheckDistance, _wallChecker.position.y));
		Gizmos.DrawLine(new Vector2(_ledgeChecker.position.x - _wallCloseCheckDistance, _ledgeChecker.position.y), new Vector2(_ledgeChecker.position.x + _wallCloseCheckDistance, _ledgeChecker.position.y));
		Gizmos.DrawLine(_groundChecker.position, new Vector2(_groundChecker.position.x, _groundChecker.position.y - _groundCheckDistance));

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(_groundChecker.position, _groundCheckRadius);
		Gizmos.DrawWireSphere(_ceilingChecker.position, _groundCheckRadius);
		Gizmos.DrawLine(new Vector2(_wallChecker.position.x - _wallCheckDistance, _wallChecker.position.y), new Vector2(_wallChecker.position.x + _wallCheckDistance, _wallChecker.position.y));
		Gizmos.DrawLine(_ledgeChecker.position, new Vector2(_ledgeChecker.position.x + facingDirection * _ledgeCheckDistance, _ledgeChecker.position.y));
		Gizmos.DrawLine(CornerCheckerPosition, new Vector2(CornerCheckerPosition.x, _wallChecker.position.y - 0.02f));
	}
}
