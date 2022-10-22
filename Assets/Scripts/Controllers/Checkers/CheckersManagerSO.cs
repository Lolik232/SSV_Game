using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CheckersManager", menuName = "Player/Checks/Checkers Manager")]

public class CheckersManagerSO : BaseScriptableObject
{
	private const float CHECK_OFFSET = 0.02f;
	private const float UNIT_SIZE = 1f;

	[SerializeField] private EntitySO _entity;

	[SerializeField] private float _groundCheckDistance;
	[SerializeField] private float _ceilingCheckDistance;
	[SerializeField] private float _wallCheckDistance;
	[SerializeField] private float _ledgeCheckDistance;
	[SerializeField] private float _groundCloseCheckDistance;
	[SerializeField] private float _wallCloseCheckDistance;

	[SerializeField] private float _wallCheckerHeight;
	[SerializeField] private float _ledgeCheckerHeight;

	[SerializeField] private LayerMask _whatIsTarget;

	[NonSerialized] public int wallDirection;

	[NonSerialized] public Vector2 wallPosition;
	[NonSerialized] public Vector2 ledgeStartPosition;
	[NonSerialized] public Vector2 ledgeEndPosition;

	[NonSerialized] public Collider2D grounded;
	[NonSerialized] public RaycastHit2D groundClose;
	[NonSerialized] public Collider2D touchingCeiling;
	[NonSerialized] public RaycastHit2D touchingCeilingWhenClimb;
	[NonSerialized] public RaycastHit2D touchingWall;
	[NonSerialized] public RaycastHit2D touchingWallBack;
	[NonSerialized] public Collider2D clampedBetweenWalls;
	[NonSerialized] public RaycastHit2D touchingLedge;
	[NonSerialized] public RaycastHit2D touchingCorner;

	private Vector2 _startLedgeOffset;
	private Vector2 _endLedgeOffset;
	private Vector2 _cornerPosition;

	private Tuple<Vector2, Vector2> _groundCheckPosition;
	private Tuple<Vector2, Vector2> _ceilingCheckPosition;
	private Tuple<Vector2, Vector2> _ceilingWhenClimbCheckPosition;
	private Tuple<Vector2, Vector2> _groundCloseCheckPosition;
	private Tuple<Vector2, Vector2> _wallCheckPosition;
	private Tuple<Vector2, Vector2> _wallBackCheckPosition;
	private Tuple<Vector2, Vector2> _clampedBetweenWallsCheckPosition;
	private Tuple<Vector2, Vector2> _clampedBetweenWallsBackCheckPosition;
	private Tuple<Vector2, Vector2> _ledgeCheckPosition;
	private Tuple<Vector2, Vector2> _cornerCheckPosition;

	protected List<UnityAction> checks = new();

	protected override void OnEnable()
	{
		base.OnEnable();

		checks = new List<UnityAction>()
		{
			() =>
			{
				CheckIfGrounded();
				CheckIfGroundClose();
				CheckIfTouchingLedge();
				CheckIfTouchingWall();
				CheckIfTouchingWallBack();
				CheckIfTouchingCeiling();
				CheckIfTouchingCeilingWhenClimb();
				CheckIfClampedBetweenWalls();
			}
		};
	}

	public void Initialize()
	{
		_startLedgeOffset = new Vector2(_entity.StandSize.x / 2 + CHECK_OFFSET, UNIT_SIZE * _ledgeCheckerHeight + CHECK_OFFSET);
		_endLedgeOffset = new Vector2(_entity.StandSize.x / 2 + CHECK_OFFSET, CHECK_OFFSET);
	}

	private void UpdateCheckersPositions()
	{
		var checkerOffset = _entity.Size / 2 - Vector2.one * CHECK_OFFSET;
		int facingRight = _entity.facingDirection;
		int facingLeft = -_entity.facingDirection;
		float rightCheckerPositionX = _entity.Center.x + facingRight * checkerOffset.x;
		float leftCheckerPositionX = _entity.Center.x + facingLeft * checkerOffset.x;
		float headHeight = _ledgeCheckerHeight - _wallCheckerHeight;

		var workspace = _entity.Center - checkerOffset;
		_groundCheckPosition = new Tuple<Vector2, Vector2>(workspace, new(_entity.Center.x + checkerOffset.x, workspace.y - _groundCheckDistance));
		workspace = new(_entity.Center.x, _entity.Center.y - checkerOffset.y);
		_groundCloseCheckPosition = new Tuple<Vector2, Vector2>(workspace, new(workspace.x, workspace.y - _groundCloseCheckDistance));

		workspace = _entity.Center + checkerOffset;
		_ceilingCheckPosition = new Tuple<Vector2, Vector2>(new(_entity.Center.x - checkerOffset.x, workspace.y + _ceilingCheckDistance), workspace);
		workspace = new Vector2(ledgeEndPosition.x, ledgeEndPosition.y + UNIT_SIZE - _ceilingCheckDistance / 2);
		_ceilingWhenClimbCheckPosition = new Tuple<Vector2, Vector2>(workspace, new(workspace.x, workspace.y + _ceilingCheckDistance));

		workspace = new Vector2(rightCheckerPositionX, _entity.Position.y + UNIT_SIZE * _wallCheckerHeight);
		_wallCheckPosition = new Tuple<Vector2, Vector2>(workspace, new(workspace.x + facingRight * _wallCheckDistance, workspace.y));
		workspace = new Vector2(leftCheckerPositionX, workspace.y);
		_wallBackCheckPosition = new Tuple<Vector2, Vector2>(workspace, new(workspace.x + facingLeft * _wallCheckDistance, workspace.y));

		workspace = new Vector2(rightCheckerPositionX, _entity.Position.y + UNIT_SIZE * _ledgeCheckerHeight);
		_ledgeCheckPosition = new Tuple<Vector2, Vector2>(workspace, new(workspace.x + facingRight * _ledgeCheckDistance, workspace.y));

		workspace = _ledgeCheckPosition.Item2;
		_cornerCheckPosition = new Tuple<Vector2, Vector2>(workspace, new(workspace.x, workspace.y - (headHeight + CHECK_OFFSET)));

		workspace = new Vector2(rightCheckerPositionX, _ledgeCheckPosition.Item1.y);
		_clampedBetweenWallsCheckPosition = new Tuple<Vector2, Vector2>(workspace, new Vector2(workspace.x + facingRight * _wallCloseCheckDistance, workspace.y - headHeight));
		workspace = new Vector2(leftCheckerPositionX, workspace.y);
		_clampedBetweenWallsBackCheckPosition = new Tuple<Vector2, Vector2>(workspace, new Vector2(workspace.x + facingLeft * _wallCloseCheckDistance, workspace.y - headHeight));
	}

	private void CheckIfGrounded()
	{
		Check(ref grounded, Physics2D.OverlapArea, _groundCheckPosition);
	}

	private void CheckIfGroundClose()
	{
		Check(ref groundClose, Physics2D.Linecast, _groundCloseCheckPosition);
	}

	private void CheckIfTouchingCeiling()
	{
		Check(ref touchingCeiling, Physics2D.OverlapArea, _ceilingCheckPosition);
	}

	private void CheckIfTouchingWall()
	{
		Check(ref touchingWall, Physics2D.Linecast, _wallCheckPosition);
		wallDirection = touchingWall ? -_entity.facingDirection : _entity.facingDirection;
		if (touchingWall)
		{
			wallPosition.Set(_wallCheckPosition.Item1.x + _entity.facingDirection * touchingWall.distance, _wallCheckPosition.Item1.y);
		}
	}

	private void CheckIfTouchingWallBack()
	{
		Check(ref touchingWallBack, Physics2D.Linecast, _wallBackCheckPosition);
	}

	private void CheckIfClampedBetweenWalls()
	{
		Check(ref clampedBetweenWalls, Physics2D.OverlapArea, _clampedBetweenWallsCheckPosition);
		if (clampedBetweenWalls)
		{
			Check(ref clampedBetweenWalls, Physics2D.OverlapArea, _clampedBetweenWallsBackCheckPosition);
		}
	}

	private void CheckIfTouchingLedge()
	{
		Check(ref touchingLedge, Physics2D.Linecast, _ledgeCheckPosition);
	}

	private void CheckIfTouchingCeilingWhenClimb()
	{
		Check(ref touchingCeilingWhenClimb, Physics2D.Linecast, _ceilingWhenClimbCheckPosition);
	}

	private void CheckIfTouchingCorner()
	{
		Check(ref touchingCorner, Physics2D.Linecast, _cornerCheckPosition);
		if (touchingCorner)
		{
			_cornerPosition.Set(wallPosition.x, _cornerCheckPosition.Item1.y - touchingCorner.distance);
		}
	}

	public void DetermineLedgePosition()
	{
		ledgeStartPosition.Set(_cornerPosition.x + wallDirection * _startLedgeOffset.x,
													 _cornerPosition.y - _startLedgeOffset.y);
		ledgeEndPosition.Set(_cornerPosition.x - wallDirection * _endLedgeOffset.x,
												 _cornerPosition.y + _endLedgeOffset.y);
	}

	public void DoChecks()
	{
		UpdateCheckersPositions();

		ApplyActions(checks);

		if (!touchingLedge && touchingWall)
		{
			CheckIfTouchingCorner();
		}
	}

	private void Check<T>(ref T isTargetDetected, Func<Vector2, Vector2, int, T> checkFunction, Tuple<Vector2, Vector2> points)
	{
			isTargetDetected = checkFunction(points.Item1, points.Item2, _whatIsTarget);
	}

	public void OnDrawGizmos()
	{
		static void DrawArea(Tuple<Vector2, Vector2> ray)
		{
			var a = ray.Item1;
			var b = ray.Item2;

			Gizmos.DrawLine(a, b);
			Gizmos.DrawWireCube((a + b) / 2, new Vector2(Mathf.Max(a.x, b.x) - Mathf.Min(a.x, b.x), Mathf.Max(a.y, b.y) - Mathf.Min(a.y, b.y)));
		}

		static void DrawLine(Tuple<Vector2, Vector2> ray)
		{
			Gizmos.DrawLine(ray.Item1, ray.Item2);
		}

		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(_entity.Position + _entity.Offset, _entity.Size);

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(ledgeEndPosition + _entity.Offset, _entity.Size);
		Gizmos.DrawWireCube(ledgeStartPosition + _entity.Offset, _entity.Size);

		Gizmos.color = Color.white;
		DrawArea(_clampedBetweenWallsCheckPosition);
		DrawArea(_clampedBetweenWallsBackCheckPosition);

		DrawLine(_groundCloseCheckPosition);

		Gizmos.color = Color.red;
		DrawLine(_ceilingWhenClimbCheckPosition);

		DrawArea(_groundCheckPosition);
		DrawArea(_ceilingCheckPosition);

		DrawLine(_wallCheckPosition);
		DrawLine(_wallBackCheckPosition);
		DrawLine(_ledgeCheckPosition);
		DrawLine(_cornerCheckPosition);
	}
}