using System;

using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInputReader", menuName = "Player/Input/Reader")]
public class PlayerInputReaderSO : BehaviourControllerSO
{
	[SerializeField] private float _jumpInputHoldTime;
	[SerializeField] private float _dashInputPressTime;

	private PlayerInput _playerInput;
	private Camera      _mainCamera;

	private float _jumpInputStartTime;
	private float _dashInputStartTime;

	private Vector2 _mouseInputPosition;

	[NonSerialized] public bool dash;

	[NonSerialized] public bool jumpInputHold;
	[NonSerialized] public bool dashInputHold;

	protected override void OnEnable()
	{
		base.OnEnable();

		updateActions.Add(() =>
		{
			lookAtPosition = _mainCamera.ScreenToWorldPoint(_mouseInputPosition);
			lookAtDirection = (lookAtPosition - entity.Center).normalized;
			lookAtDistance = (lookAtPosition - entity.Center).magnitude;

			jump &= Time.time < _jumpInputStartTime + _jumpInputHoldTime;
			dash &= Time.time < _dashInputStartTime + _dashInputPressTime;
		});
	}

	public void Initialize(PlayerInput playerInput, Camera camera)
	{
		_playerInput = playerInput;
		_mainCamera = camera;
	}

	public void OnMoveInput(InputAction.CallbackContext context)
	{
		move = Vector2Int.RoundToInt(context.ReadValue<Vector2>());
	}

	public void OnGrabInput(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			grab = true;
		}
		else if (context.canceled)
		{
			grab = false;
		}
	}

	public void OnDashInput(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			dash = true;
			dashInputHold = true;
			_dashInputStartTime = Time.time;
		}
		else if (context.canceled)
		{
			dash = false;
			dashInputHold = false;
		}
	}

	public void OnAttackInput(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			attack = true;
		}
		else if (context.canceled)
		{
			attack = false;
		}
	}

	public void OnAbilityInput(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			ability = true;
		}
		else if (context.canceled)
		{
			ability = false;
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
			jump = true;
			jumpInputHold = true;
			_jumpInputStartTime = Time.time;
		}
		else if (context.canceled)
		{
			jumpInputHold = false;
		}
	}
}