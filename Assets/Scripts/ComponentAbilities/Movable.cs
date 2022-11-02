using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Movable : MonoBehaviour, IMovable
{
	private readonly Cacheable<Vector2> _position = new();
	private readonly Cacheable<Vector2> _velocity = new();
	private readonly Cacheable<float> _gravity = new();

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

	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (_position.IsLocked)
		{
			transform.position = _position.Value;		
		}
	}

	public void TrySetPosition(Vector2 position)
	{
		if (_position.TrySet(position))
		{
			transform.position = _position.Value;
		}
	}

	public void TrySetPositionX(float x)
	{
		if (_position.TrySet(new Vector2(x, Position.y)))
		{
			transform.position = _position.Value;
		}
	}

	public void TrySetPositionY(float y)
	{
		if (_position.TrySet(new Vector2(Position.x, y)))
		{
			transform.position = _position.Value;
		}
	}

	public void TrySetVelocity(Vector2 velocity)
	{
		if (_velocity.TrySet(velocity))
		{
			_rb.velocity = _velocity.Value;
		}
	}

	public void TrySetVelocity(float speed, Vector2 angle, int direction)
	{
		if (_velocity.TrySet(new Vector2(angle.normalized.x * speed * direction, angle.normalized.y * speed)))
		{
			_rb.velocity = _velocity.Value;
		}
	}

	public void TrySetVelocityX(float x)
	{
		if (_velocity.TrySet(new Vector2(x, Velocity.y)))
		{
			_rb.velocity = _velocity.Value;
		}
	}

	public void TrySetVelocityY(float y)
	{
		if (_velocity.TrySet(new Vector2(Velocity.x, y)))
		{
			_rb.velocity = _velocity.Value;
		}
	}

	public void TrySetGravity(float gravity)
	{
		if (_gravity.TrySet(gravity))
		{
			_rb.gravityScale = _gravity.Value;
		}
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

	public int HoldVelocity(float speed, Vector2 angle, int direction)
	{
		int id =  _velocity.Hold(new Vector2(angle.normalized.x * speed * direction, angle.normalized.y * speed));
		_rb.velocity = _velocity.Value;
		return id;
	}
}
