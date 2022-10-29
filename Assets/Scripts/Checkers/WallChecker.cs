using UnityEngine;

[RequireComponent(typeof(Movable), typeof(Physical))]

public class WallChecker : BaseMonoBehaviour, IWallChecker
{
	[SerializeField] private LayerMask _whatIsTarget;
	[SerializeField] private float _wallCheckDistance;
	[SerializeField] private float _wallCheckerYOffset;

	private bool _touchingWall;
	private bool _touchingWallBack;
	private CheckArea _wallCheckRay;
	private CheckArea _wallBackCheckRay;

	private Vector2 _wallPosition;
	private int _wallDirection;

	private Physical _physical;
	private Movable _movable;

	public bool TouchingWall
	{
		get => _touchingWall;
	}
	public bool TouchingWallBack
	{
		get => _touchingWallBack;
	}
	public Vector2 WallPosition
	{
		get => _wallPosition;
	}
	public int WallDirection
	{
		get => _wallDirection;
	}

	protected override void Awake()
	{
		base.Awake();
		_physical = GetComponent<Physical>();
		_movable = GetComponent<Movable>();
	}

	public void DoChecks()
	{
		RaycastHit2D hit = Physics2D.Linecast(_wallCheckRay.a, _wallCheckRay.b, _whatIsTarget);
		_touchingWall = hit;
		_wallDirection = _touchingWall ? -_movable.FacingDirection : _movable.FacingDirection;
		if (_touchingWall)
		{
			_wallPosition = hit.point;
		}

		_touchingWallBack = Physics2D.Linecast(_wallBackCheckRay.a, _wallBackCheckRay.b, _whatIsTarget);
	}

	public void UpdateCheckersPosition()
	{
		Vector2 checkerOffset = _physical.Size / 2 - Vector2.one * IChecker.CHECK_OFFSET;
		float rightCheckerPositionX = _physical.Center.x + _movable.FacingDirection * checkerOffset.x;
		float leftCheckerPositionX = _physical.Center.x - _movable.FacingDirection * checkerOffset.x;

		Vector2 workspace = new(rightCheckerPositionX, _physical.Position.y + _wallCheckerYOffset);
		_wallCheckRay = new CheckArea(workspace.x,
																	 workspace.y,
																	 workspace.x + _movable.FacingDirection * _wallCheckDistance,
																	 workspace.y);
		workspace = new Vector2(leftCheckerPositionX, workspace.y);
		_wallBackCheckRay = new CheckArea(workspace.x,
																			 workspace.y,
																			 workspace.x - _movable.FacingDirection * _wallCheckDistance,
																			 workspace.y);
	}

	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
		Utility.DrawArea(_wallCheckRay, TouchingWall, Color.gray);
		Utility.DrawArea(_wallBackCheckRay, TouchingWallBack, Color.gray);
	}
}
