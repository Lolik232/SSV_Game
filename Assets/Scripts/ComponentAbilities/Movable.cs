using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Movable : BaseMonoBehaviour, IMovable
{
	[SerializeField] private Parameter _moveUpSpeed;
	[SerializeField] private Parameter _moveDownSpeed;
	[SerializeField] private Parameter _moveForwardSpeed;
	[SerializeField] private Parameter _moveBackwardSpeed;

	private readonly Cacheable<Vector2> _position = new();
	private readonly Cacheable<Vector2> _velocity = new();
	private readonly Cacheable<float> _gravity = new();
	private readonly Cacheable<int> _facingDirection = new();

	private int _bodyDirection;

	private Rigidbody2D _rb;

	public Vector2 Position
	{
		get => transform.position;
	}
	public Vector2 Velocity
	{
		get => _rb.velocity;
	}
	public float Gravity
	{
		get => _rb.gravityScale;
	}
	public int FacingDirection
	{
		get => _facingDirection.Value;
	}
	public int BodyDirection
	{
		get => _bodyDirection;
	}
	public Parameter MoveUpSpeed
	{
		get => _moveUpSpeed;
		set => _moveUpSpeed.Set(value);
	}
	public Parameter MoveDownSpeed
	{
		get => _moveDownSpeed;
		set => _moveDownSpeed.Set(value);
	}
	public Parameter MoveForwardSpeed
	{
		get => _moveForwardSpeed;
		set => _moveForwardSpeed.Set(value);
	}
	public Parameter MoveBackwardSpeed
	{
		get => _moveBackwardSpeed;
		set => _moveBackwardSpeed.Set(value);
	}

	protected override void Awake()
	{
		base.Awake();
		_rb = GetComponent<Rigidbody2D>();

		_moveUpSpeed.Initialize();
		_moveDownSpeed.Initialize();
		_moveForwardSpeed.Initialize();
		_moveBackwardSpeed.Initialize();
	}

	protected override void Start()
	{
		base.Start();

		TryRotateIntoDirection(1);
	}

	public void TryRotateIntoDirection(int direction)
	{
		if (direction != 0)
		{
			if (_facingDirection.TrySet(direction))
			{
				RotateBodyIntoDirection(direction);
			}
		}
	}

	public void RotateBodyIntoDirection(int direction)
	{
		if (direction != 0)
		{
			_bodyDirection = direction;
			switch (direction)
			{
				case 1:
					transform.rotation = Quaternion.Euler(0f, 0f, 0f);
					break;
				case -1:
					transform.rotation = Quaternion.Euler(0f, 180f, 0f);
					break;
				default:
					break;
			}
		}
	}

	public void TrySetPosition(Vector2 position)
	{
		_position.TrySet(position);
	}

	public void TrySetPositionX(float x)
	{
		_position.TrySet(new Vector2(x, Position.y));
	}

	public void TrySetPositionY(float y)
	{
		_position.TrySet(new Vector2(Position.x, y));
	}

	public void TrySetVelocity(Vector2 velocity)
	{
		_velocity.TrySet(velocity);
	}

	public void TrySetVelocity(float speed, Vector2 angle, int direction)
	{
		_velocity.TrySet(new Vector2(angle.normalized.x * speed * direction, angle.normalized.y * speed));
	}

	public void TrySetVelocityX(float x)
	{
		_velocity.TrySet(new Vector2(x, Velocity.y));
	}

	public void TrySetVelocityY(float y)
	{
		_velocity.TrySet(new Vector2(Velocity.x, y));
	}

	public void TrySetGravity(float gravity)
	{
		_gravity.TrySet(gravity);
	}

	public int HoldPosition(Vector2 position)
	{
		int id = _position.Hold(position);
		transform.position = _position.Value;
		return id;
	}

	public int HoldVelocity(Vector2 velocity)
	{
		int id =  _velocity.Hold(velocity);
		_rb.velocity = _velocity.Value;
		return id;
	}

	public int HoldGravity(float gravity)
	{
		int id = _gravity.Hold(gravity);
		_rb.gravityScale = _gravity.Value;
		return id;
	}

	public int HoldDirection(int direction)
	{
		int id = _facingDirection.Hold(direction);
		RotateBodyIntoDirection(direction);
		return id;
	}

	public void ReleasePosition(int id)
	{
		_position.Release(id);
		transform.position = _position.Value;
	}

	public void ReleaseVelocity(int id)
	{
		_velocity.Release(id);
		_rb.velocity = new Vector2(_velocity.Value.x, Velocity.y);
	}

	public void ReleaseGravity(int id)
	{
		_gravity.Release(id);
		_rb.gravityScale = _gravity.Value;
	}

	public void ReleaseDirection(int id)
	{
		_facingDirection.Release(id);
		RotateBodyIntoDirection(FacingDirection);
	}

	public int HoldVelocity(float speed, Vector2 angle, int direction)
	{
		int id =  _velocity.Hold(new Vector2(angle.normalized.x * speed * direction, angle.normalized.y * speed));
		_rb.velocity = _velocity.Value;
		return id;
	}
}
