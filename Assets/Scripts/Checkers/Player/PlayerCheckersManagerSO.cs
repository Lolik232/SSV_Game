using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCheckersManager", menuName = "Managers/Checks/Player")]

public class PlayerCheckersManagerSO : CheckersManagerSO
{
	[SerializeField] private float _ledgeCheckDistance;
	[SerializeField] private float _groundCloseCheckDistance;
	[SerializeField] private float _wallCloseCheckDistance;

	[SerializeField] private float _ledgeCheckerHeight;

	[NonSerialized] public Vector2 ledgeStartPosition;
	[NonSerialized] public Vector2 ledgeEndPosition;

	[NonSerialized] public bool groundClose;
	[NonSerialized] public bool touchingCeilingWhenClimb;
	[NonSerialized] public bool clampedBetweenWalls;
	[NonSerialized] public bool touchingLedge;
	[NonSerialized] public bool touchingCorner;

	private Tuple<Vector2, Vector2> _ceilingWhenClimbCheckPosition;
	private Tuple<Vector2, Vector2> _groundCloseCheckPosition;
	private Tuple<Vector2, Vector2> _clampedBetweenWallsBackCheckPosition;
	private Tuple<Vector2, Vector2> _clampedBetweenWallsCheckPosition;
	private Tuple<Vector2, Vector2> _ledgeCheckPosition;
	private Tuple<Vector2, Vector2> _cornerCheckPosition;

	private Vector2 _startLedgeOffset;
	private Vector2 _endLedgeOffset;
	private Vector2 _cornerPosition;

	private float _headHeight;

	protected override void OnEnable()
	{
		base.OnEnable();

		checks.Add(() =>
		{
			CheckIfGroundClose();
			CheckIfTouchingLedge();
			CheckIfTouchingCeilingWhenClimb();
			CheckIfClampedBetweenWalls();

			if (!touchingLedge && touchingWall)
			{
				CheckIfTouchingCorner();
			}
		});

		drawGizmosActions.Add(() =>
		{
			Gizmos.color = Color.white;
			Utility.DrawArea(_clampedBetweenWallsBackCheckPosition);
			Utility.DrawArea(_clampedBetweenWallsCheckPosition);

			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(ledgeEndPosition + physical.Offset, physical.Size);
			Gizmos.DrawWireCube(ledgeStartPosition + physical.Offset, physical.Size);

			Utility.DrawLine(_groundCloseCheckPosition);
			Gizmos.color = Color.red;
			Utility.DrawLine(_ceilingWhenClimbCheckPosition);
			Utility.DrawLine(_ledgeCheckPosition);
			Utility.DrawLine(_cornerCheckPosition);

		});
	}

	public override void Initialize(GameObject origin)
	{
		base.Initialize(origin);

		_startLedgeOffset = new Vector2(crouchable.StandSize.x / 2 + CHECK_OFFSET, UNIT_SIZE * _ledgeCheckerHeight + CHECK_OFFSET);
		_endLedgeOffset = new Vector2(crouchable.StandSize.x / 2 + CHECK_OFFSET, CHECK_OFFSET);
	}

	protected override void UpdateCheckersPositions()
	{
		base.UpdateCheckersPositions();

		_headHeight = _ledgeCheckerHeight - wallCheckerHeight;
		workspace = new(physical.Center.x, physical.Center.y - checkerOffset.y);
		_groundCloseCheckPosition = new Tuple<Vector2, Vector2>(workspace, new(workspace.x, workspace.y - _groundCloseCheckDistance));
		workspace = new Vector2(ledgeEndPosition.x, ledgeEndPosition.y + UNIT_SIZE - ceilingCheckDistance / 2);
		_ceilingWhenClimbCheckPosition = new Tuple<Vector2, Vector2>(workspace, new(workspace.x, workspace.y + ceilingCheckDistance));
		workspace = new Vector2(rightCheckerPositionX, physical.Position.y + UNIT_SIZE * _ledgeCheckerHeight);
		_ledgeCheckPosition = new Tuple<Vector2, Vector2>(workspace, new(workspace.x + facingRight * _ledgeCheckDistance, workspace.y));

		workspace = _ledgeCheckPosition.Item2;
		_cornerCheckPosition = new Tuple<Vector2, Vector2>(workspace, new(workspace.x, workspace.y - (_headHeight + CHECK_OFFSET)));
		workspace = new Vector2(rightCheckerPositionX, _ledgeCheckPosition.Item1.y);
		_clampedBetweenWallsCheckPosition = new Tuple<Vector2, Vector2>(workspace, new Vector2(workspace.x + facingRight * _wallCloseCheckDistance, workspace.y - _headHeight));
		workspace = new Vector2(leftCheckerPositionX, workspace.y);
		_clampedBetweenWallsBackCheckPosition = new Tuple<Vector2, Vector2>(workspace, new Vector2(workspace.x + facingLeft * _wallCloseCheckDistance, workspace.y - _headHeight));
	}

	private void CheckIfGroundClose()
	{
		groundClose = Utility.Check(Physics2D.Linecast, _groundCloseCheckPosition, whatIsTarget);
	}

	private void CheckIfClampedBetweenWalls()
	{
		clampedBetweenWalls = Utility.Check(Physics2D.OverlapArea, _clampedBetweenWallsCheckPosition, whatIsTarget);
		if (clampedBetweenWalls)
		{
			clampedBetweenWalls = Utility.Check(Physics2D.OverlapArea, _clampedBetweenWallsBackCheckPosition, whatIsTarget);
		}
	}

	private void CheckIfTouchingLedge()
	{
		touchingLedge = Utility.Check(Physics2D.Linecast, _ledgeCheckPosition, whatIsTarget);
	}

	private void CheckIfTouchingCeilingWhenClimb()
	{
		touchingCeilingWhenClimb = Utility.Check(Physics2D.Linecast, _ceilingWhenClimbCheckPosition, whatIsTarget);
	}

	private void CheckIfTouchingCorner()
	{
		RaycastHit2D hit = Utility.Check(Physics2D.Linecast, _cornerCheckPosition, whatIsTarget);
		touchingCorner = hit;
		if (touchingCorner)
		{
			_cornerPosition.Set(wallPosition.x, _cornerCheckPosition.Item1.y - hit.distance);
		}
	}

	public void DetermineLedgePosition()
	{
		ledgeStartPosition.Set(_cornerPosition.x + wallDirection * _startLedgeOffset.x,
													 _cornerPosition.y - _startLedgeOffset.y);
		ledgeEndPosition.Set(_cornerPosition.x - wallDirection * _endLedgeOffset.x,
												 _cornerPosition.y + _endLedgeOffset.y);
	}
}
