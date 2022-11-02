using UnityEngine;

[RequireComponent(typeof(Rotateable), typeof(Physical))]

public class LedgeChecker : MonoBehaviour, ILedgeChecker
{
	[SerializeField] private LayerMask _whatIsTarget;
	[SerializeField] private float _ledgeCheckDistance;
	[SerializeField] private float _ledgeCheckerYOffset;
	[SerializeField] private PickableColor _color;

	private bool _touchingLedge;
	private CheckArea _ledgeCheckRay;

	private Physical _physical;
	private Rotateable _rotateable;

	public bool TouchingLegde
	{
		get => _touchingLedge;
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

	private void OnDrawGizmos()
	{
		Utility.DrawArea(_ledgeCheckRay, TouchingLegde, _color.Color);
	}

	public void DoChecks()
	{
		_touchingLedge = Physics2D.Linecast(_ledgeCheckRay.a, _ledgeCheckRay.b, _whatIsTarget);
	}

	public void UpdateCheckersPosition()
	{
		Vector2 checkerOffset = _physical.Size / 2 - Vector2.one * IChecker.CHECK_OFFSET;
		float rightCheckerPositionX = _physical.Center.x + _rotateable.FacingDirection * checkerOffset.x;

		Vector2 workspace = new(rightCheckerPositionX, _physical.Position.y + _ledgeCheckerYOffset);
		_ledgeCheckRay = new CheckArea(workspace.x,
																		workspace.y,
																		workspace.x + _rotateable.FacingDirection * _ledgeCheckDistance,
																		workspace.y);
	}
}

