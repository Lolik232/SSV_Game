using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCheckersManagerBase : BaseMonoBehaviour
{
	[SerializeField] private PlayerCheckersManagerSO _checkersManager;

	private Player _player;

	protected override void Awake()
	{
		_player = GetComponent<Player>();
		_checkersManager.Initialize(_player);

		base.Awake();

		drawGizmosActions.Add(() =>
		{
			_checkersManager.OnDrawGizmos();
		});
	}
}
