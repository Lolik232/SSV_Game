using UnityEngine;

[RequireComponent(typeof(Physical))]

public class CeilChecker : MonoBehaviour, ICeilChecker
{
	[SerializeField] private LayerMask _whatIsTarget;
	[SerializeField] private float _ceilCheckDistance;
	[SerializeField] private PickableColor _color;

	private bool _touchingCeiling;
	private CheckArea _ceilCheckArea;

	private Physical _physical;

	public bool TouchingCeiling
	{
		get => _touchingCeiling;
	}

	private void Awake()
	{
		_physical = GetComponent<Physical>();
	}

	private void OnDrawGizmos()
	{
		Utility.DrawArea(_ceilCheckArea, TouchingCeiling, _color.Color);
	}

	private void FixedUpdate()
	{
		UpdateCheckersPosition();
		DoChecks();
	}

	public void DoChecks()
	{
		_touchingCeiling = Physics2D.OverlapArea(_ceilCheckArea.a, _ceilCheckArea.b, _whatIsTarget);
	}

	public void UpdateCheckersPosition()
	{
		Vector2 checkerOffset = _physical.Size / 2 - Vector2.one * IChecker.CHECK_OFFSET;

		Vector2 workspace = _physical.Center + checkerOffset;
		_ceilCheckArea = new CheckArea(_physical.Center.x - checkerOffset.x,
																		workspace.y + _ceilCheckDistance,
																		workspace.x,
																		workspace.y);
	}
}
