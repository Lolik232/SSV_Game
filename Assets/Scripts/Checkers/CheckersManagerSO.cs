using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public abstract class CheckersManagerSO : BaseScriptableObject
{
	protected const float CHECK_OFFSET = 0.02f;
	protected const float UNIT_SIZE = 1f;

	[SerializeField] protected EntitySO entity;

	[SerializeField] protected float groundCheckDistance;
	[SerializeField] protected float ceilingCheckDistance;
	[SerializeField] protected float wallCheckDistance;

	[SerializeField] protected float wallCheckerHeight;
	
	[NonSerialized] public int wallDirection;

	[NonSerialized] public Vector2 wallPosition;
	[NonSerialized] public Vector2 ledgeStartPosition;
	[NonSerialized] public Vector2 ledgeEndPosition;

	[NonSerialized] public Collider2D grounded;
	[NonSerialized] public Collider2D touchingCeiling;
	[NonSerialized] public RaycastHit2D touchingWall;
	[NonSerialized] public RaycastHit2D touchingWallBack;

	public LayerMask whatIsTarget;

	protected Tuple<Vector2, Vector2> groundCheckPosition;
	protected Tuple<Vector2, Vector2> ceilingCheckPosition;
	protected Tuple<Vector2, Vector2> wallCheckPosition;
	protected Tuple<Vector2, Vector2> wallBackCheckPosition;

	protected List<UnityAction> checks = new();

	protected Vector2 workspace;
	protected Vector2 checkerOffset;
	protected int facingRight;
	protected int facingLeft;
	protected float rightCheckerPositionX;
	protected float leftCheckerPositionX;

	protected override void OnEnable()
	{
		base.OnEnable();

		checks = new List<UnityAction>()
		{
			() =>
			{
				CheckIfGrounded();
				CheckIfTouchingWall();
				CheckIfTouchingWallBack();
				CheckIfTouchingCeiling();
			}
		};

		drawGizmosActions.Add(() =>
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(entity.Position + entity.Offset, entity.Size);

			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(ledgeEndPosition + entity.Offset, entity.Size);
			Gizmos.DrawWireCube(ledgeStartPosition + entity.Offset, entity.Size);

			Gizmos.color = Color.red;
			Utility.DrawArea(groundCheckPosition);
			Utility.DrawArea(ceilingCheckPosition);

			Utility.DrawLine(wallCheckPosition);
			Utility.DrawLine(wallBackCheckPosition);
		});
	}

	public virtual void Initialize()
	{
	
	}

	protected virtual void UpdateCheckersPositions()
	{
		checkerOffset = entity.Size / 2 - Vector2.one * CHECK_OFFSET;
		facingRight = entity.facingDirection;
		facingLeft = -entity.facingDirection;
		rightCheckerPositionX = entity.Center.x + facingRight * checkerOffset.x;
		leftCheckerPositionX = entity.Center.x + facingLeft * checkerOffset.x;

		workspace = entity.Center - checkerOffset;
		groundCheckPosition = new Tuple<Vector2, Vector2>(workspace, new(entity.Center.x + checkerOffset.x, workspace.y - groundCheckDistance));

		workspace = entity.Center + checkerOffset;
		ceilingCheckPosition = new Tuple<Vector2, Vector2>(new(entity.Center.x - checkerOffset.x, workspace.y + ceilingCheckDistance), workspace);
	
		workspace = new Vector2(rightCheckerPositionX, entity.Position.y + UNIT_SIZE * wallCheckerHeight);
		wallCheckPosition = new Tuple<Vector2, Vector2>(workspace, new(workspace.x + facingRight * wallCheckDistance, workspace.y));
		workspace = new Vector2(leftCheckerPositionX, workspace.y);
		wallBackCheckPosition = new Tuple<Vector2, Vector2>(workspace, new(workspace.x + facingLeft * wallCheckDistance, workspace.y));
	}

	private void CheckIfGrounded()
	{
		grounded = Utility.Check(Physics2D.OverlapArea, groundCheckPosition, whatIsTarget);
	}

	private void CheckIfTouchingCeiling()
	{
		touchingCeiling = Utility.Check(Physics2D.OverlapArea, ceilingCheckPosition, whatIsTarget);
	}

	private void CheckIfTouchingWall()
	{
		touchingWall = Utility.Check(Physics2D.Linecast, wallCheckPosition, whatIsTarget);
		wallDirection = touchingWall ? -entity.facingDirection : entity.facingDirection;
		if (touchingWall)
		{
			wallPosition.Set(wallCheckPosition.Item1.x + entity.facingDirection * touchingWall.distance, wallCheckPosition.Item1.y);
		}
	}

	private void CheckIfTouchingWallBack()
	{
		touchingWallBack = Utility.Check(Physics2D.Linecast, wallBackCheckPosition, whatIsTarget);
	}

	public void DoChecks()
	{
		UpdateCheckersPositions();
		Utility.ApplyActions(checks);
	}
}