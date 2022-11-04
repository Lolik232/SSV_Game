using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Movable : MonoBehaviour, IMovable
{
	private readonly Blocker _positionBlocker = new();
	private readonly Blocker _velocityBlocker = new();

	private float _cachedGravity;

	private Rigidbody2D _rb;

	public Vector2 Position
	{
		get => transform.position;
		private set => transform.position = value;
	}
	public Vector2 Velocity
	{
		get => _rb.velocity;
		private set => _rb.velocity = value;
	}
	public float Gravity
	{
		get => _rb.gravityScale;
		private set => _rb.gravityScale = value;
	}
	public bool IsPositionLocked
	{
		get => _positionBlocker.IsLocked;
	}
	public bool IsVelocityLocked
	{
		get => _velocityBlocker.IsLocked;
	}

	public void BlockPosition()
	{
		_positionBlocker.AddBlock();
	}

	public void BlockVelocity()
	{
		_velocityBlocker.AddBlock();
	}

	public void ResetGravity()
	{
		Gravity = _cachedGravity;
	}

	public void SetGravity(float gravity)
	{
		_cachedGravity = Gravity;
		Gravity = gravity;
	}

	public void SetPosition(Vector2 position)
	{
		Position = position;
	}

	public void SetPosition(float x, float y)
	{
		Position = new Vector2(x, y);
	}

	public void SetpositionX(float x)
	{
		Position = new Vector2(x, Position.y);
	}

	public void SetpositionY(float y)
	{
		Position = new Vector2(Position.x, y);
	}

	public void SetVelocity(Vector2 velocity)
	{
		Velocity = velocity;
	}

	public void SetVelocity(float x, float y)
	{
		Velocity = new Vector2(x, y);
	}

	public void SetVelocity(float speed, Vector2 angle, int direction)
	{
		Velocity = new Vector2(direction * angle.normalized.x * speed, angle.normalized.y * speed);
	}

	public void SetVelocityX(float x)
	{
		Velocity = new Vector2(x, Velocity.y);
	}

	public void SetVelocityY(float y)
	{
		Velocity = new Vector2(Velocity.x, y);
	}

	public void UnlockPosition()
	{
		_positionBlocker.RemoveBlock();
	}

	public void UnlockVelocity()
	{
		_positionBlocker.AddBlock();
	}

	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
	}
}
