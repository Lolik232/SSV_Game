using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCheckersManagerBase : MonoBehaviour
{
	[SerializeField] private PlayerCheckersManagerSO _checkersManager;

	private Player _player;

	private bool _initialized = false;

	private void Awake()
	{
		_player = GetComponent<Player>();
	}

	private void Start()
	{
		_checkersManager.Initialize(_player);
		_initialized = true;
	}

	private void OnDrawGizmos()
	{
		if (_initialized)
		{
			_checkersManager.OnDrawGizmos();
		}
	}
}
