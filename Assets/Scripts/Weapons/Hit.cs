using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : BaseMonoBehaviour
{
	private Animator anim;

	private readonly Blocker _positionBlocker = new();

	private Vector2 _holdPosition;

	public Vector2 Position => transform.position;

	protected override void Awake()
	{
		anim = GetComponent<Animator>();

		base.Awake();

		lateUpdateActions.Add(() =>
		{
			if (_positionBlocker.IsLocked)
			{
				transform.position = _holdPosition;
			}
		});
	}

	public void OnHit(Vector2 hitposition)
	{
		HoldPosition(hitposition);
		anim.SetTrigger("hit");
	}

	private void HoldPosition(Vector2 holdPosition)
	{
		_holdPosition = holdPosition;
		transform.position = holdPosition;
		_positionBlocker.AddBlock();
	}

	private void ReleasePosition()
	{
		_positionBlocker.RemoveBlock();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(Position, 0.2f);
	}
}
