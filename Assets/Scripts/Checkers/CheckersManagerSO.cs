using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public abstract class CheckersManagerSO : StaticManagerSO<bool>
{
	protected const float CHECK_OFFSET = 0.02f;
	protected const float UNIT_SIZE = 1f;

	[SerializeField] protected float groundCheckDistance;
	[SerializeField] protected float ceilingCheckDistance;
	[SerializeField] protected float wallCheckDistance;

	[SerializeField] protected float wallCheckerHeight;

	[NonSerialized] public int wallDirection;

	[NonSerialized] public Vector2 wallPosition;

	[NonSerialized] public bool grounded;
	[NonSerialized] public bool touchingCeiling;
	[NonSerialized] public bool touchingWall;
	[NonSerialized] public bool touchingWallBack;

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

	protected Physical physical;
	protected Movable movable;
	protected Crouchable crouchable;

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
			Gizmos.DrawWireCube(physical.Position + physical.Offset, physical.Size);

			Gizmos.color = Color.red;
			Utility.DrawArea(groundCheckPosition);
			Utility.DrawArea(ceilingCheckPosition);

			Utility.DrawLine(wallCheckPosition);
			Utility.DrawLine(wallBackCheckPosition);
		});
	}

	public override void Initialize(GameObject origin)
	{
		base.Initialize(origin);
		physical = origin.GetComponent<Physical>();
		movable = origin.GetComponent<Movable>();
		crouchable = origin.GetComponent<Crouchable>();
	}

	protected virtual void UpdateCheckersPositions()
	{
		checkerOffset = physical.Size / 2 - Vector2.one * CHECK_OFFSET;
		facingRight = movable.FacingDirection;
		facingLeft = -movable.FacingDirection;
		rightCheckerPositionX = physical.Center.x + facingRight * checkerOffset.x;
		leftCheckerPositionX = physical.Center.x + facingLeft * checkerOffset.x;

		workspace = physical.Center - checkerOffset;
		groundCheckPosition = new Tuple<Vector2, Vector2>(workspace, new(physical.Center.x + checkerOffset.x, workspace.y - groundCheckDistance));

		workspace = physical.Center + checkerOffset;
		ceilingCheckPosition = new Tuple<Vector2, Vector2>(new(physical.Center.x - checkerOffset.x, workspace.y + ceilingCheckDistance), workspace);

		workspace = new Vector2(rightCheckerPositionX, physical.Position.y + UNIT_SIZE * wallCheckerHeight);
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
		RaycastHit2D hit = Utility.Check(Physics2D.Linecast, wallCheckPosition, whatIsTarget);
		touchingWall = hit;
		wallDirection = touchingWall ? -movable.FacingDirection : movable.FacingDirection;
		if (touchingWall)
		{
			wallPosition.Set(wallCheckPosition.Item1.x + movable.FacingDirection * hit.distance, wallCheckPosition.Item1.y);
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