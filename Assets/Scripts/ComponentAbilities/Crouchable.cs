using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Crouchable : MonoBehaviour, ICrouchable
{
	[SerializeField] private Vector2 _staySize;
	[SerializeField] private Vector2 _stayOffset;
	[Space]
	[SerializeField] private Vector2 _crouchSize;
	[SerializeField] private Vector2 _crouchOffset;

	private BoxCollider2D _collider;

	public Vector2 StandSize
	{
		get => _staySize;
	}
	public Vector2 StandOffset
	{
		get => _stayOffset;
	}
	public Vector2 StandCenter
	{
		get => (Vector2)transform.position + _stayOffset;
	}
	public Vector2 CrouchSize
	{
		get => _crouchSize;
	}
	public Vector2 CrouchOffset
	{
		get => _crouchOffset;
	}
	public Vector2 CrouchCenter
	{
		get => (Vector2)transform.position + _crouchOffset;
	}
	public bool IsStanding
	{
		get;
		private set;
	}

	public void Crouch()
	{
		IsStanding = false;
		_collider.size = CrouchSize;
		_collider.offset = CrouchOffset;
	}

	public void Stand()
	{
		IsStanding = true;
		_collider.size = StandSize;
		_collider.offset = StandOffset;
	}

	private void Awake()
	{
		_collider = GetComponent<BoxCollider2D>();
	}
}
