using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponBase : MonoBehaviour
{
	[SerializeField] private PlayerWeaponSO _weapon;

	[SerializeField] private GameObject baseUnit;

	private Animator _baseAnim;
	private Animator _anim;
	private Player _player;
	private Hit _hit;

	private bool _initilized = false;

	protected virtual void Awake()
	{
		_player = baseUnit.GetComponent<Player>();
		_baseAnim = baseUnit.GetComponent<Animator>();
		_anim = GetComponent<Animator>();
		_hit = GetComponentInChildren<Hit>();
	}

	protected virtual void Start()
	{
		_weapon.Initialize(_player, _baseAnim, _anim, _hit);
		_initilized = true;
	}

	protected virtual void OnDrawGizmos()
	{
		if (_initilized)
		{
			_weapon.OnDrawGizmos();
		}
	}
}
