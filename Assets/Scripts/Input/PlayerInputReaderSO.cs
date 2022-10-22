using System;

using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInputReader", menuName = "Player/Input/Reader")]
public class PlayerInputReaderSO : ScriptableObject
{
	[SerializeField] private float _jumpInputHoldTime;
	[SerializeField] private float _dashInputPressTime;

	private PlayerInput _playerInput;
	private Camera      _mainCamera;

	private float _jumpInputStartTime;
	private float _dashInputStartTime;

	private Vector2 _mouseInputPosition;

	[NonSerialized] public bool jumpInput;
	[NonSerialized] public bool grabInput;
	[NonSerialized] public bool dashInput;
	[NonSerialized] public bool attackInput;
	[NonSerialized] public bool abilityInput;

	[NonSerialized] public bool jumpInputHold;
	[NonSerialized] public bool dashInputHold;

	[NonSerialized] public Vector2Int moveInput;

	[NonSerialized] public Vector2 mouseInputPosition;
	[NonSerialized] public Vector2 mouseInputDirection;
	[NonSerialized] public float   mouseInputDistance;

	public void OnUpdate()
	{
		mouseInputPosition = _mainCamera.ScreenToWorldPoint(_mouseInputPosition);

		jumpInput &= Time.time < _jumpInputStartTime + _jumpInputHoldTime;
		dashInput &= Time.time < _dashInputStartTime + _dashInputPressTime;
	}

	public void InitializePlayerInput(PlayerInput playerInput) => _playerInput = playerInput;
	public void InitializeCamera(Camera camera) => _mainCamera = camera;

	public void InitializePlayerCamera(Camera camera) => _mainCamera = camera;

	public void OnMoveInput(InputAction.CallbackContext context)
	{
		moveInput = Vector2Int.RoundToInt(context.ReadValue<Vector2>());
	}

	public void OnGrabInput(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			grabInput = true;
		}
		else if (context.canceled)
		{
			grabInput = false;
		}
	}

	public void OnDashInput(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			dashInput           = true;
			dashInputHold       = true;
			_dashInputStartTime = Time.time;
		}
		else if (context.canceled)
		{
			dashInput     = false;
			dashInputHold = false;
		}
	}

	public void OnAttackInput(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			attackInput = true;
		}
		else if (context.canceled)
		{
			attackInput = false;
		}
	}

	public void OnAbilityInput(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			abilityInput = true;
		}
		else if (context.canceled)
		{
			abilityInput = false;
		}
	}

	public void OnDirectionInput(InputAction.CallbackContext context)
	{
		if (_playerInput.currentControlScheme == "Keyboard")
		{
			_mouseInputPosition = context.ReadValue<Vector2>();
		}
	}

	public void OnJumpInput(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			jumpInput           = true;
			jumpInputHold       = true;
			_jumpInputStartTime = Time.time;
		}
		else if (context.canceled)
		{
			jumpInputHold = false;
		}
	}
}