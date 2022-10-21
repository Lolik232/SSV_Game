using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReaderBase : MonoBehaviour
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
		inputReader.InitializeCamera(Camera.main);
	}

	private void Update()
	{
		inputReader.OnUpdate();
		inputReader.mouseInputDirection = (inputReader.mouseInputPosition - _player.Center).normalized;
		inputReader.mouseInputDistance = (inputReader.mouseInputPosition - _player.Center).magnitude;
	}
}
