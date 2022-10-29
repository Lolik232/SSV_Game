using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveController : MoveController
{
	private Vector2 _mouseInputPosition;

	private PlayerInput _playerInput;

	protected override void Awake()
	{
		base.Awake();
		_playerInput = GetComponent<PlayerInput>();
	}

	protected override void Update()
	{
		base.Update();
		lookAt = Camera.main.ScreenToWorldPoint(_mouseInputPosition);
	}

	public void OnMoveInput(InputAction.CallbackContext context)
	{
		move = Vector2Int.RoundToInt(context.ReadValue<Vector2>());
	}

	public void OnDirectionInput(InputAction.CallbackContext context)
	{
		if (_playerInput.currentControlScheme == "Keyboard")
		{
			_mouseInputPosition = context.ReadValue<Vector2>();
		}
	}
}
