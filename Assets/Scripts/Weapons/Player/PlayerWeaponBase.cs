using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponBase : BaseMonoBehaviour
{
	[SerializeField] private PlayerWeaponSO _weapon;

	[SerializeField] private GameObject baseUnit;

	private Animator _baseAnim;
	private Animator _anim;
	private Player _player;
	private Hit _hit;

	protected override void Awake()
	{
		_player = baseUnit.GetComponent<Player>();
		_baseAnim = baseUnit.GetComponent<Animator>();
		_anim = GetComponent<Animator>();
		_hit = GetComponentInChildren<Hit>();

		base.Awake();

		startActions.Add(() =>
		{
			_weapon.Initialize(_player, _baseAnim, _anim, _hit);
		});

		drawGizmosActions.Add(() =>
		{
			_weapon.OnDrawGizmos();
		});
	}
}
