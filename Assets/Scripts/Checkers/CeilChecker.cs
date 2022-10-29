using UnityEngine;

[RequireComponent(typeof(Physical), typeof(Crouchable))]

public class CeilChecker : BaseMonoBehaviour, ICeilChecker
{
	[SerializeField] private LayerMask _whatIsTarget;
	[SerializeField] private float _ceilCheckDistance;

	private bool _touchingCeiling;
	private CheckArea _ceilCheckArea;

	private Physical _physical;
	private Crouchable _crouchable;

	public bool TouchingCeiling
	{
		get => _touchingCeiling;
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
																		_physical.Position.y + _crouchable.StandSize.y + _ceilCheckDistance,
																		workspace.x,
																		workspace.y);
	}

	protected override void Awake()
	{
		base.Awake();
		_physical = GetComponent<Physical>();
		_crouchable = GetComponent<Crouchable>();
	}

	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
		Utility.DrawArea(_ceilCheckArea, TouchingCeiling, Color.gray);
	}
}
