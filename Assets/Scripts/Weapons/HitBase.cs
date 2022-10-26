using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBase : BaseMonoBehaviour
{
	[SerializeField] private HitSO _hit;

	private Animator _anim;

	protected override void Awake()
	{
		_anim = GetComponent<Animator>();
		_hit.Initialize(transform, _anim);

		base.Awake();

		lateUpdateActions.Add(() =>
		{
			_hit.OnLateUpdate();
		});

		drawGizmosActions.Add(() =>
		{
			_hit.OnDrawGizmos();
		});
	}

	private void OnAnimationFinishTrigger() => _hit.ReleasePosition();
}
