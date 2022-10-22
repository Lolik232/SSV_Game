using System;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(PlayerInputReaderBase), typeof(PlayerAbilitiesManagerBase), typeof(PlayerParametersManagerBase))]
[RequireComponent(typeof(PlayerCheckersManagerBase), typeof(InventoryBase))]
[RequireComponent(typeof(PlayerStateMachineBase), typeof(Rigidbody2D), typeof(Animator))]

public class Player : BaseMonoBehaviour
{
	[SerializeField] private Vector2 _standOffset;
	[SerializeField] private Vector2 _standSize;
	[SerializeField] private Vector2 _crouchOffset;
	[SerializeField] private Vector2 _crouchSize;

	[NonSerialized] public int facingDirection = 1;

	[NonSerialized] public bool isStanding;

	private BoxCollider2D _col;
	private Rigidbody2D _rb;
	private TrailRenderer _tr;

	private readonly Blocker _positionBlocker = new();
	private readonly Blocker _velocityBlocker = new();
	private readonly Blocker _gravityBlocker = new();
	private readonly Blocker _directionBlocker = new();

	private Vector2 _holdPosition;
	private Vector2 _cachedVelocity;
	private float _cachedGravity;

	public Vector2 Velocity => _rb.velocity;

	public Vector2 Position => transform.position;

	public Vector2 Size => _col.size;

	public Vector2 Offset => _col.offset;

	public Vector2 Center => Position + Offset;

	public Vector2 StandOffset => _standOffset;

	public Vector2 StandSize => _standSize;

	public Vector2 StandCenter => Position + StandOffset;

	public Vector2 CrouchSize => _crouchSize;

	public Vector2 CrouchOffset => _crouchOffset;

	public Vector2 CrouchCenter => Position + CrouchOffset;

	public float Gravity => _rb.gravityScale;

	protected override void Awake()
	{
		base.Awake();

		_tr = GetComponent<TrailRenderer>();
		_rb = GetComponent<Rigidbody2D>();
		_col = GetComponent<BoxCollider2D>();

		startActions.Add(() =>
		{
			Stand();
		});

		updateActions.Add(() =>
		{
			if (_positionBlocker.IsLocked)
			{
				transform.position = _holdPosition;
			}
		});
	}

	public void CheckIfShouldFlip(int direction)
	{
		if (facingDirection != direction && direction != 0 && !_directionBlocker.IsLocked)
		{
			HardFlip();
		}
	}

	public void HardFlip()
	{
		facingDirection = -facingDirection;
		SoftFlip();
	}

	public void SoftFlip()
	{
		transform.Rotate(0f, 180f, 0f);
	}

	public void TrySetVelocity(float velocity, Vector2 angle, int direction)
	{
		TrySetVelocity(new Vector2(angle.normalized.x * velocity * direction, angle.normalized.y * velocity));
	}

	public void TrySetVelocityX(float xVelocity)
	{
		TrySetVelocity(new Vector2(xVelocity, _rb.velocity.y));
	}

	public void TrySetVelocityY(float yVelocity)
	{
		TrySetVelocity(new Vector2(_rb.velocity.x, yVelocity));
	}

	public void TrySetVelocityZero()
	{
		TrySetVelocity(Vector2.zero);
	}

	public void TrySetVelocity(Vector2 velocity)
	{
		if (!_velocityBlocker.IsLocked)
		{
			_rb.velocity = velocity;
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
			_rb.gravityScale = gravity;
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
			CacheVelocity(_rb.velocity);
		}

		_rb.velocity = velocity;
		_velocityBlocker.AddBlock();
	}

	public void ReleaseVelocity()
	{
		_velocityBlocker.RemoveBlock();
		if (!_velocityBlocker.IsLocked)
		{
			_rb.velocity = _cachedVelocity;
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
			CacheGravity(_rb.gravityScale);
		}

		_rb.gravityScale = gravity;
		_gravityBlocker.AddBlock();
	}

	public void ReleaseGravity()
	{
		_gravityBlocker.RemoveBlock();
		if (!_gravityBlocker.IsLocked)
		{
			_rb.gravityScale = _cachedGravity;
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
		_col.size = _standSize;
		_col.offset = _standOffset;
	}

	public void Crouch()
	{
		isStanding = false;
		_col.size = _crouchSize;
		_col.offset = _crouchOffset;
	}

	public void EnableTrail()
	{
		_tr.emitting = true;
	}

	public void DisableTrail()
	{
		_tr.emitting = false;
		_tr.Clear();
	}
}
