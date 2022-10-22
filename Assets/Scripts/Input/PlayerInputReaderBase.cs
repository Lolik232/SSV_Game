using System;
using System.Security;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReaderBase : BaseMonoBehaviour
{
	private Player _player;

	public PlayerInputReaderSO inputReader;

	protected override void Awake()
	{
		_player = GetComponent<Player>();

		base.Awake();

		startActions.Add(() =>
		{
			inputReader.InitializePlayerInput(GetComponent<PlayerInput>());
			inputReader.InitializePlayerCamera(Camera.main);
			inputReader.InitializeCamera(Camera.main);
		});

		updateActions.Add(() =>
		{
			inputReader.OnUpdate();
			inputReader.mouseInputDirection = (inputReader.mouseInputPosition - _player.Center).normalized;
			inputReader.mouseInputDistance = (inputReader.mouseInputPosition - _player.Center).magnitude;
		});
	}
}