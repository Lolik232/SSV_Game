using UnityEngine;

[RequireComponent(typeof(Physical), typeof(Crouchable))]

public class CeilChecker : MonoBehaviour, ICeilChecker
{
	[SerializeField] private LayerMask _whatIsTarget;
	[SerializeField] private float _ceilCheckDistance;
	[SerializeField] private PickableColor _color;

	private CheckArea _ceilCheckArea;

	private Physical _physical;
	private Crouchable _crouchable;

	public bool TouchingCeiling
	{
		get;
		private set;
	}

	private void Awake()
	{
		_physical = GetComponent<Physical>();
		_crouchable = GetComponent<Crouchable>();
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
		TouchingCeiling = Physics2D.OverlapArea(_ceilCheckArea.a, _ceilCheckArea.b, _whatIsTarget);
		if (!_crouchable.IsStanding)
		{
			_crouchable.CanStand = !TouchingCeiling;
		}
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
