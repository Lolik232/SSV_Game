using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class WeaponBase : BaseMonoBehaviour
{
	[SerializeField] private WeaponSO _weapon;
	[SerializeField] private GameObject _baseObject;

	private Animator _baseAnim;
	private Animator _anim;

	protected override void Awake()
	{
		_baseAnim = _baseObject.GetComponent<Animator>();
		_anim = GetComponent<Animator>();
		_weapon.Initialize(_baseAnim, _anim);

		base.Awake();

		drawGizmosActions.Add(() =>
		{
			_weapon.OnDrawGizmos();
		});
	}
}
