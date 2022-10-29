using System;

using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInputReader", menuName = "Player/Input/Reader")]
public class PlayerInputReaderSO : MoveController
{
	[SerializeField] private float _jumpInputHoldTime;
	[SerializeField] private float _dashInputPressTime;

	private PlayerInput _playerInput;
	private Camera      _mainCamera;

	private float _jumpInputStartTime;
	private float _dashInputStartTime;

	private Vector2 _mouseInputPosition;

	[NonSerialized] public bool jump;
	[NonSerialized] public bool grab;
	[NonSerialized] public bool attack;
	[NonSerialized] public bool ability;
	[NonSerialized] public bool dash;

	[NonSerialized] public bool jumpInputHold;
	[NonSerialized] public bool dashInputHold;

	protected Physical physical;

	protected override void OnEnable()
	{
		base.OnEnable();

		updateActions.Add(() =>
		{
			lookAtPosition = _mainCamera.ScreenToWorldPoint(_mouseInputPosition);
			lookAtDirection = (lookAtPosition - physical.Center).normalized;
			lookAtDistance = (lookAtPosition - physical.Center).magnitude;

			jump &= Time.time < _jumpInputStartTime + _jumpInputHoldTime;
			dash &= Time.time < _dashInputStartTime + _dashInputPressTime;
		});
	}

	public override void Initialize(GameObject origin)
	{
		base.Initialize(origin);
		physical = origin.GetComponent<Physical>();
		_playerInput = origin.GetComponent<PlayerInput>();
		_mainCamera = Camera.main;
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