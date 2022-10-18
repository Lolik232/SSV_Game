using System;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReaderOwner : MonoBehaviour
{
	private Player _player;

	public PlayerInputReaderSO inputReader;

	private void Awake()
	{
		_player = GetComponent<Player>();
	}

	private void Start()
	{
		inputReader.InitializePlayerInput(GetComponent<PlayerInput>());
		inputReader.InitializePlayerCamera(Camera.main);
	}

	private void Update()
	{
		inputReader.OnUpdate();
		inputReader.mouseInputDirection = (inputReader.mouseInputPosition - _player.Center).normalized;
		inputReader.mouseInputDistance  = (inputReader.mouseInputPosition - _player.Center).magnitude;
	}
}