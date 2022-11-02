using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Crouchable : MonoBehaviour, ICrouchable
{
	[SerializeField] private Vector2 _standSize;
	[SerializeField] private Vector2 _standOffset;
	[Space]
	[SerializeField] private Vector2 _crouchSize;
	[SerializeField] private Vector2 _crouchOffset;

	private bool _isStanding;

	private BoxCollider2D _collider;

	public Vector2 StandSize
	{
		get => _standSize;
	}
	public Vector2 StandOffset
	{
		get => _standOffset;
	}
	public Vector2 StandCenter
	{
		get => (Vector2)transform.position + _standOffset;
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
		get => _isStanding;
	}

	public void Crouch()
	{
		_isStanding = false;
		_collider.size = CrouchSize;
		_collider.offset = CrouchOffset;
	}

	public void Stand()
	{
		_isStanding = true;
		_collider.size = StandSize;
		_collider.offset = StandOffset;
	}

	private void Awake()
	{
		_collider = GetComponent<BoxCollider2D>();
	}

	private void Start()
	{
		Stand();
	}
}
