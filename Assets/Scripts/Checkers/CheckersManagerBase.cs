using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class CheckersManagerBase : BaseMonoBehaviour
{
	[SerializeField] private CheckersManagerSO _checkersManager;

	protected override void Awake()
	{
		_checkersManager.Initialize();

		base.Awake();

		startActions.Add(() =>
		{
			_checkersManager.OnEnter();
		});

		drawGizmosActions.Add(() =>
		{
			_checkersManager.OnDrawGizmos();
		});
	}
}
