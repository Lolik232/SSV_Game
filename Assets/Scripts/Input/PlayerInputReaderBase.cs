using System;
using System.Security;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReaderBase : BaseMonoBehaviour
{
	[SerializeField] private PlayerInputReaderSO _inputReader;

	private PlayerInput _input;

	protected override void Awake()
	{
		_input = GetComponent<PlayerInput>();

		base.Awake();

		startActions.Add(() =>
		{
			_inputReader.Initialize(_input, Camera.main);
		});

		updateActions.Add(() =>
		{
			_inputReader.OnUpdate();
		});
	}
}