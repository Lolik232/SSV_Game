using UnityEngine;

[RequireComponent(typeof(Physical), typeof(Rotateable), typeof(Damageable))]

public class Dummy : Entity, IPhysical, IRotateable , IDamageable
{
	private Physical _physical;
	private Rotateable _rotateable;
	private Damageable _damageable;

	public Vector2 Position => _physical.Position;

	public Vector2 Velocity => _physical.Velocity;

	public float Gravity => _physical.Gravity;

	public Vector2 Size => _physical.Size;

	public Vector2 Offset => _physical.Offset;

	public Vector2 Center => _physical.Center;

	public int FacingDirection => _rotateable.FacingDirection;

	public int BodyDirection => _rotateable.BodyDirection;

	public float MaxHealth
	{
		get => _damageable.MaxHealth;
		set => _damageable.MaxHealth = value;
	}

	public float Health => _damageable.Health;

	public void Push(float force, Vector2 angle)
	{
		_physical.Push(force, angle);
	}

	public void RestoreHealth(float regeneration)
	{
		_damageable.RestoreHealth(regeneration);
	}

	public void RotateBodyIntoDirection(int direction)
	{
		_rotateable.RotateBodyIntoDirection(direction);
	}

	public void RotateIntoDirection(int direction)
	{
		_rotateable.RotateIntoDirection(direction);
	}

	public void TakeDamage(float damage)
	{
		_damageable.TakeDamage(damage);
	}

	private void Awake()
	{
		_physical = GetComponent<Physical>();
		_rotateable = GetComponent<Rotateable>();
		_damageable = GetComponent<Damageable>();
	}

	private void Start()
	{
		RotateIntoDirection(1);
	}
}
