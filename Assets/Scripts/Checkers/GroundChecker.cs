using UnityEngine;

[RequireComponent(typeof(Physical))]

public class GroundChecker : MonoBehaviour, IGroundChecker
{
	[SerializeField] private LayerMask _whatIsTarget;
	[SerializeField] private float _groundCheckDistance;
	[SerializeField] private PickableColor _color;

	private bool _grounded;
	private CheckArea _groundCheckArea;

	private Physical _physical;

	public bool Grounded
	{
		get => _grounded;
	}

	public void DoChecks()
	{
		_grounded = Physics2D.OverlapArea(_groundCheckArea.a, _groundCheckArea.b, _whatIsTarget);
	}

	public void UpdateCheckersPosition()
	{
		Vector2 checkerOffset = _physical.Size / 2 - Vector2.one * IChecker.CHECK_OFFSET;

		Vector2 workspace = _physical.Center - checkerOffset;
		_groundCheckArea = new CheckArea(workspace.x,
																		 workspace.y,
																		 _physical.Center.x + checkerOffset.x,
																		 workspace.y - _groundCheckDistance);
	}

	private void Awake()
	{
		_physical = GetComponent<Physical>();
	}

	private void OnDrawGizmos()
	{
		Utility.DrawArea(_groundCheckArea, Grounded, _color.Color);
	}

	private void FixedUpdate()
	{
		UpdateCheckersPosition();
		DoChecks();
	}
}
