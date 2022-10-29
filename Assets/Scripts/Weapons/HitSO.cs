using UnityEngine;

[CreateAssetMenu(fileName = "Hit", menuName = "Weapons/Hit")]
public class HitSO : AnimatedComponentSO
{
	private Transform _transform;

	private readonly Blocker _positionBlocker = new();

	private Vector2 _holdPosition;

	public Vector2 Position => _transform.position;

	protected override void OnEnable()
	{
		base.OnEnable();

		lateUpdateActions.Add(() =>
		{
			if (_positionBlocker.IsLocked)
			{
				_transform.position = _holdPosition;
			}
		});

		drawGizmosActions.Add(() =>
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(Position, 0.2f);
		});
	}

	public override void InitialzeBase(GameObject baseObject)
	{
		base.InitialzeBase(baseObject);
		_transform = baseObject.transform;
	}

	public void OnHit(Vector2 hitposition)
	{
		HoldPosition(hitposition);
		anim.SetTrigger("hit");
	}

	public void HoldPosition(Vector2 holdPosition)
	{
		_holdPosition = holdPosition;
		_transform.position = holdPosition;
		_positionBlocker.AddBlock();
	}

	public void ReleasePosition()
	{
		_positionBlocker.RemoveBlock();
	}
}
