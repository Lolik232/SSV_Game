using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

public abstract class EntitySO : ComponentSO
{
	public const int DIRECTION_LEFT = -1;
	public const int DIRECTION_RIGHT = 1;

	public AbilitiesManagerSO abilities;
	public StateMachineSO states;
	public CheckersManagerSO checkers;
	public ParametersManagerSO parameters;
	public BehaviourControllerSO controller;

	[SerializeField] private Vector2 _standOffset;
	[SerializeField] private Vector2 _standSize;
	[SerializeField] private Vector2 _crouchOffset;
	[SerializeField] private Vector2 _crouchSize;

	[NonSerialized] public int facingDirection;
	[NonSerialized] public int realDirection;

	[NonSerialized] public bool isStanding;

	private Transform _transform;
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

	public Vector2 Position => _transform.position;

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

	protected override void OnEnable()
	{
		base.OnEnable();

		enterActions.Add(() =>
		{
			Stand();
			RotateIntoDirection(DIRECTION_RIGHT);
		});

		updateActions.Add(() =>
		{
			if (_positionBlocker.IsLocked)
			{
				_transform.position = _holdPosition;
			}
		});
	}

	public override void InitialzeBase(GameObject baseObject)
	{
		base.InitialzeBase(baseObject);

		_transform = baseObject.GetComponent<Transform>();
		_rb = baseObject.GetComponent<Rigidbody2D>();
		_col = baseObject.GetComponent<BoxCollider2D>();
		_tr = baseObject.GetComponent<TrailRenderer>();
	}

	public void TryRotateIntoDirection(int direction)
	{
		if (_directionBlocker.IsLocked)
		{
			return;
		}

		RotateIntoDirection(direction);
	}

	public void RotateIntoDirection(int direction)
	{
		if (direction == 0)
		{
			return;
		}

		facingDirection = direction;
		RotateBodyIntoDirection(direction);
	}

	public void RotateBodyIntoDirection(int direction)
	{
		if (direction == 0)
		{
			return;
		}

		realDirection = direction;
		switch (direction)
		{
			case DIRECTION_RIGHT:
				_transform.rotation = Quaternion.Euler(0f, 0f, 0f);
				break;
			case DIRECTION_LEFT:
				_transform.rotation = Quaternion.Euler(0f, 180f, 0f);
				break;
			default:
				break;
		}
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

	public void HoldVelocity(Vector2 velocit)
	{
		if (!_velocityBlocker.IsLocked)
		{
			CacheVelocity(_rb.velocity);
		}

		_rb.velocity = velocit;
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
		RotateIntoDirection(direction);
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
		_transform.position = holdPosition;
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

	public void MoveTo(Vector2 position)
	{
		_transform.position = position;
	}

	public void MoveToX(float x)
	{
		_transform.position = new Vector2(x, _transform.position.y);
	}

	public void MoveToY(float y)
	{
		_transform.position = new Vector2(_transform.position.x, y);
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
