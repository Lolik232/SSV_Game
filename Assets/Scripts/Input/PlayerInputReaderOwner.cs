using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReaderOwner : MonoBehaviour
{
	private Player _player;

	public PlayerInputReaderSO inputReader;

	private void Awake()
	{
		_player = GetComponent<Player>();
		inputReader.InitializePlayerInput(GetComponent<PlayerInput>());
	}

	private void Update()
	{
		inputReader.OnUpdate();
		inputReader.mouseInputDirection = (inputReader.mouseInputPosition - _player.Center).normalized;
		inputReader.mouseInputDistance = (inputReader.mouseInputPosition - _player.Center).magnitude;
	}
}
