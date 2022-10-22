using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AbilitiesManagerBase), typeof(PlayerParametersManagerBase))]
[RequireComponent(typeof(CheckersManagerBase), typeof(InventoryBase))]
[RequireComponent(typeof(StateMachineBase), typeof(Rigidbody2D), typeof(Animator))]

public class EntityBase : BaseMonoBehaviour
{
	[SerializeField]  private EntitySO _entity;

	private BoxCollider2D _col;
	private Rigidbody2D _rb;
	private TrailRenderer _tr;

	protected override void Awake()
	{
		_tr = GetComponent<TrailRenderer>();
		_rb = GetComponent<Rigidbody2D>();
		_col = GetComponent<BoxCollider2D>();
		_entity.Initialize(transform, _rb,	_col, _tr);

		base.Awake();

		startActions.Add(() =>
		{
			_entity.Stand();
		});

		updateActions.Add(() =>
		{
			_entity.OnUpdate();
		});
	}
}

	
