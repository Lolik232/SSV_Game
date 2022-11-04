using UnityEngine;

[RequireComponent(typeof(Rotateable), typeof(Physical))]

public class WallChecker : MonoBehaviour, IWallChecker
{
	[SerializeField] private LayerMask _whatIsTarget;
	[SerializeField] private float _wallCheckDistance;
	[SerializeField] private float _wallCheckerYOffset;
	[SerializeField] private PickableColor _color;

	private CheckArea _wallCheckRay;
	private CheckArea _wallBackCheckRay;

	private Physical _physical;
	private Rotateable _rotateable;

	public bool TouchingWall
	{
		get; private set;
	}
	public bool TouchingWallBack
	{
		get; private set;
	}
	public Vector2 WallPosition
	{
		get; private set;
	}
	public int WallDirection
	{
		get; private set;
	}
	public float YOffset
	{
		get => _wallCheckerYOffset;
	}

	private void Awake()
	{
		_physical = GetComponent<Physical>();
		_rotateable = GetComponent<Rotateable>();
	}

	private void FixedUpdate()
	{
		UpdateCheckersPosition();
		DoChecks();
	}

	public void DoChecks()
	{
		RaycastHit2D hit = Physics2D.Linecast(_wallCheckRay.a, _wallCheckRay.b, _whatIsTarget);
		TouchingWall = hit;
		WallDirection = TouchingWall ? -_rotateable.FacingDirection : _rotateable.FacingDirection;
		if (TouchingWall)
		{
			WallPosition = hit.point;
		}

		TouchingWallBack = Physics2D.Linecast(_wallBackCheckRay.a, _wallBackCheckRay.b, _whatIsTarget);
	}

	public void UpdateCheckersPosition()
	{
		Vector2 checkerOffset = _physical.Size / 2 - Vector2.one * IChecker.CHECK_OFFSET;
		float rightCheckerPositionX = _physical.Center.x + _rotateable.FacingDirection * checkerOffset.x;
		float leftCheckerPositionX = _physical.Center.x - _rotateable.FacingDirection * checkerOffset.x;

		Vector2 workspace = new(rightCheckerPositionX, _physical.Position.y + _wallCheckerYOffset);
		_wallCheckRay = new CheckArea(workspace.x,
																	 workspace.y,
																	 workspace.x + _rotateable.FacingDirection * _wallCheckDistance,
																	 workspace.y);
		workspace = new Vector2(leftCheckerPositionX, workspace.y);
		_wallBackCheckRay = new CheckArea(workspace.x,
																			 workspace.y,
																			 workspace.x - _rotateable.FacingDirection * _wallCheckDistance,
																			 workspace.y);
	}

	private void OnDrawGizmos()
	{
		Utility.DrawArea(_wallCheckRay, TouchingWall, _color.Color);
		Utility.DrawArea(_wallBackCheckRay, TouchingWallBack, _color.Color);
	}
}