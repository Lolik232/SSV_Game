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
		_inputReader.Initialize(_input, Camera.main);

		base.Awake();

		startActions.Add(() =>
		{
			_inputReader.OnEnter();
		});

		updateActions.Add(() =>
		{
			_inputReader.OnUpdate();
		});
	}
}