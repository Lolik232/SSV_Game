using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCheckersManager", menuName = "Player/Checks/Checkers Manager")]

public class PlayerCheckersManagerSO : ScriptableObject
{
	private const float CHECK_OFFSET = 0.02f;
	private const float UNIT_SIZE = 1f;

	[SerializeField] private float _groundCheckDistance;
	[SerializeField] private float _ceilingCheckDistance;
	[SerializeField] private float _wallCheckDistance;
	[SerializeField] private float _ledgeCheckDistance;
	[SerializeField] private float _groundCloseCheckDistance;
	[SerializeField] private float _wallCloseCheckDistance;

	[SerializeField] private float _wallCheckerHeight;
	[SerializeField] private float _ledgeCheckerHeight;

	[SerializeField] private List<LayerMask> _whatIsTarget;

	[NonSerialized] public int wallDirection;

	[NonSerialized] public Vector2 wallPosition;
	[NonSerialized] public Vector2 ledgeStartPosition;
	[NonSerialized] public Vector2 ledgeEndPosition;

	[NonSerialized] public bool isGrounded;
	[NonSerialized] public bool isGroundClose;
	[NonSerialized] public bool isTouchingCeiling;
	[NonSerialized] public bool isTouchingCeilingWhenClimb;
	[NonSerialized] public bool isTouchingWall;
	[NonSerialized] public bool isTouchingWallBack;
	[NonSerialized] public bool isClampedBetweenWalls;
	[NonSerialized] public bool isTouchingLedge;

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

	private Player _player;

	public void Initialize(Player player)
	{
		_player = player;
		_startLedgeOffset = new Vector2(_player.StandSize.x / 2 + CHECK_OFFSET, UNIT_SIZE * _ledgeCheckerHeight + CHECK_OFFSET);
		_endLedgeOffset = new Vector2(_player.StandSize.x / 2 + CHECK_OFFSET, CHECK_OFFSET);
	}

	private void UpdateCheckersPositions()
	{

		var checkerOffset = _player.Size / 2 - Vector2.one * CHECK_OFFSET;
		int facingRight = _player.facingDirection;
		int facingLeft = -_player.facingDirection;
		float rightCheckerPositionX = _player.Center.x + facingRight * checkerOffset.x;
		float leftCheckerPositionX = _player.Center.x + facingLeft * checkerOffset.x;
		float headHeight = _ledgeCheckerHeight - _wallCheckerHeight;

		var workspace = _player.Center - checkerOffset;
		_groundCheckPosition = new Tuple<Vector2, Vector2>(workspace, new(_player.Center.x + checkerOffset.x, workspace.y - _groundCheckDistance));
		workspace = new(_player.Center.x, _player.Center.y - checkerOffset.y);
		_groundCloseCheckPosition = new Tuple<Vector2, Vector2>(workspace, new(workspace.x, workspace.y - _groundCloseCheckDistance));

		workspace = _player.Center + checkerOffset;
		_ceilingCheckPosition = new Tuple<Vector2, Vector2>(new(_player.Center.x - checkerOffset.x, workspace.y + _ceilingCheckDistance), workspace);
		workspace = new Vector2(ledgeEndPosition.x, ledgeEndPosition.y + UNIT_SIZE - _ceilingCheckDistance / 2);
		_ceilingWhenClimbCheckPosition = new Tuple<Vector2, Vector2>(workspace, new(workspace.x, workspace.y + _ceilingCheckDistance));


		workspace = new Vector2(rightCheckerPositionX, _player.Position.y + UNIT_SIZE * _wallCheckerHeight);
		_wallCheckPosition = new Tuple<Vector2, Vector2>(workspace, new(workspace.x + facingRight * _wallCheckDistance, workspace.y));
		workspace = new Vector2(leftCheckerPositionX, workspace.y);
		_wallBackCheckPosition = new Tuple<Vector2, Vector2>(workspace, new(workspace.x + facingLeft * _wallCheckDistance, workspace.y));

		workspace = new Vector2(rightCheckerPositionX, _player.Position.y + UNIT_SIZE * _ledgeCheckerHeight);
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
		foreach (var target in _whatIsTarget)
		{
			isGrounded = Physics2D.OverlapArea(_groundCheckPosition.Item1, _groundCheckPosition.Item2, target);
			if (isGrounded)
			{
				return;
			}
		}
	}

	private void CheckIfGroundClose()
	{
		foreach (var target in _whatIsTarget)
		{
			isGroundClose = Physics2D.Linecast(_groundCloseCheckPosition.Item1, _groundCloseCheckPosition.Item2, target);
			if (isGroundClose)
			{
				return;
			}
		}
	}

	private void CheckIfTouchingCeiling()
	{
		foreach (var target in _whatIsTarget)
		{
			isTouchingCeiling = Physics2D.OverlapArea(_ceilingCheckPosition.Item1, _ceilingCheckPosition.Item2, target);
			if (isTouchingCeiling)
			{
				return;
			}
		}
	}

	private void CheckIfTouchingWall()
	{
		foreach (var target in _whatIsTarget)
		{
			RaycastHit2D hit = Physics2D.Linecast(_wallCheckPosition.Item1, _wallCheckPosition.Item2, target);
			wallDirection = hit ? -_player.facingDirection : _player.facingDirection;
			isTouchingWall = hit;
			if (isTouchingWall)
			{
				wallPosition.Set(_wallCheckPosition.Item1.x + _player.facingDirection * hit.distance, _wallCheckPosition.Item1.y);
				return;
			}
		}
	}

	private void CheckIfTouchingWallBack()
	{
		foreach (var target in _whatIsTarget)
		{
			isTouchingWallBack = Physics2D.Linecast(_wallBackCheckPosition.Item1, _wallBackCheckPosition.Item2, target);
			if (isTouchingWallBack)
			{
				return;
			}
		}
	}

	private void CheckIfClampedBetweenWalls()
	{
		foreach (var target in _whatIsTarget)
		{
			isClampedBetweenWalls = Physics2D.OverlapArea(_clampedBetweenWallsCheckPosition.Item1, _clampedBetweenWallsCheckPosition.Item2, target) &&
															Physics2D.OverlapArea(_clampedBetweenWallsBackCheckPosition.Item1, _clampedBetweenWallsBackCheckPosition.Item2, target);
			if (isClampedBetweenWalls)
			{
				return;
			}
		}
	}

	private void CheckIfTouchingLedge()
	{
		foreach (var target in _whatIsTarget)
		{
			isTouchingLedge = Physics2D.Linecast(_ledgeCheckPosition.Item1, _ledgeCheckPosition.Item2, target);
			if (isTouchingLedge)
			{
				return;
			}
		}
	}

	private void CheckIfTouchingCeilingWhenClimb()
	{
		foreach (var target in _whatIsTarget)
		{
			isTouchingCeilingWhenClimb = Physics2D.Linecast(_ceilingWhenClimbCheckPosition.Item1, _ceilingWhenClimbCheckPosition.Item2, target);
			if (isTouchingCeilingWhenClimb)
			{
				return;
			}
		}
	}

	private void DetermineCornerPosition()
	{
		foreach (var target in _whatIsTarget)
		{
			RaycastHit2D hit = Physics2D.Linecast(_cornerCheckPosition.Item1, _cornerCheckPosition.Item2, target);
			if (hit)
			{
				_cornerPosition.Set(wallPosition.x, _cornerCheckPosition.Item1.y - hit.distance);
				return;
			}
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

		CheckIfGrounded();
		CheckIfGroundClose();
		CheckIfTouchingLedge();
		CheckIfTouchingWall();
		CheckIfTouchingWallBack();
		CheckIfTouchingCeiling();
		CheckIfTouchingCeilingWhenClimb();
		CheckIfClampedBetweenWalls();

		if (!isTouchingLedge && isTouchingWall)
		{
			DetermineCornerPosition();
		}
	}

	public void OnDrawGizmos()
	{
		static void DrawArea(Tuple<Vector2, Vector2> ray)
		{
			var a = ray.Item1;
			var b = ray.Item2;

			Gizmos.DrawLine(a, b);

			if (a.x > b.x)
			{
				Vector2 t = a;
				a.Set(b.x, a.y);
				b.Set(t.x, b.y);
			}

			if (a.y > b.y)
			{
				Vector2 t = a;
				a.Set(a.x, b.y);
				b.Set(b.x, t.y);
			}

			Gizmos.DrawWireCube((a + b) / 2, b - a);
		}

		static void DrawLine(Tuple<Vector2, Vector2> ray)
		{
			Gizmos.DrawLine(ray.Item1, ray.Item2);
		}

		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(_player.Position + _player.Offset, _player.Size);

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(ledgeEndPosition + _player.Offset, _player.Size);
		Gizmos.DrawWireCube(ledgeStartPosition + _player.Offset, _player.Size);

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
