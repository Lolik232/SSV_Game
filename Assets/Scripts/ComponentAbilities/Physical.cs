using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]

public class Physical : BaseMonoBehaviour, IPhysical
{
	private BoxCollider2D _collider;
	private Rigidbody2D _rb;

	public Vector2 Size
	{
		get => _collider.size;
	}
	public Vector2 Offset
	{
		get => _collider.offset;
	}
	public Vector2 Center
	{
		get => (Vector2)transform.position + _collider.offset;
	}
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

	protected override void Awake()
	{
		base.Awake();
		_collider = GetComponent<BoxCollider2D>();
		_rb = GetComponent<Rigidbody2D>();
	}
}
