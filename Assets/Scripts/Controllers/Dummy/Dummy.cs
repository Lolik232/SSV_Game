using UnityEngine;

[RequireComponent(typeof(Physical), typeof(Rotateable))]

public class Dummy : Entity, IPhysical, IRotateable
{
	private Physical _physical;
	private Rotateable _rotateable;

	public Vector2 Position => _physical.Position;

	public Vector2 Velocity => _physical.Velocity;

	public float Gravity => _physical.Gravity;

	public Vector2 Size => _physical.Size;

	public Vector2 Offset => _physical.Offset;

	public Vector2 Center => _physical.Center;

	public int FacingDirection => _rotateable.FacingDirection;

	public int BodyDirection => _rotateable.BodyDirection;

	public void Push(float force, Vector2 angle)
	{
		_physical.Push(force, angle);
	}

	public void RotateBodyIntoDirection(int direction)
	{
		_rotateable.RotateBodyIntoDirection(direction);
	}

	public void RotateIntoDirection(int direction)
	{
		_rotateable.RotateIntoDirection(direction);
	}

	private void Awake()
	{
		_physical = GetComponent<Physical>();
		_rotateable = GetComponent<Rotateable>();
	}

	private void Start()
	{
		RotateIntoDirection(1);
	}
}
