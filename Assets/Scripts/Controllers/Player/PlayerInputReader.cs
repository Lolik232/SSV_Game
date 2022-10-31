using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MoveController), typeof(JumpController), typeof(DashController))]
[RequireComponent(typeof(GrabController), typeof(AttackController), typeof(AbilityController))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerInputReader : BaseMonoBehaviour
{
	[SerializeField] private float _jumpInputHoldTime;
	[SerializeField] private float _dashInputPressTime;

	private Vector2 _mouseInputPosition;

	private Camera _camera;
	private PlayerInput _playerInput;
	private MoveController _moveController;
	private JumpController _jumpController;
	private DashController _dashController;
	private GrabController _grabController;
	private AttackController _attackController;
	private AbilityController _abilityController;

	private float _jumpInputStartTime;
	private float _dashInputStartTime;

	protected override void Awake()
	{
		base.Awake();
		_camera = Camera.main;
		_playerInput = GetComponent<PlayerInput>();
		_moveController = GetComponent<MoveController>();
		_jumpController = GetComponent<JumpController>();
		_dashController = GetComponent<DashController>();
		_grabController = GetComponent<GrabController>();
		_attackController = GetComponent<AttackController>();
		_abilityController = GetComponent<AbilityController>();
	}

	protected override void Update()
	{
		base.Update();
		_moveController.LookAt = _camera.ScreenToWorldPoint(_mouseInputPosition);
		_jumpController.Jump &= Time.time < _jumpInputStartTime + _jumpInputHoldTime;
		_dashController.Dash &= Time.time < _dashInputStartTime + _dashInputPressTime;
	}

	public void OnMoveInput(InputAction.CallbackContext context)
	{
		_moveController.Move = Vector2Int.RoundToInt(context.ReadValue<Vector2>());
	}

	public void OnGrabInput(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			_grabController.Grab = true;
		}
		else if (context.canceled)
		{
			_grabController.Grab = false;
		}
	}

	public void OnDashInput(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			_dashController.Dash = true;
			_dashInputStartTime = Time.time;
		}
		else if (context.canceled)
		{
			_dashController.Dash = false;
		}
	}

	public void OnAttackInput(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			_attackController.Attack = true;
		}
		else if (context.canceled)
		{
			_attackController.Attack = false;
		}
	}

	public void OnAbilityInput(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			_abilityController.Ability = true;
		}
		else if (context.canceled)
		{
			_abilityController.Ability = false;
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
			_jumpController.Jump = true;
			_jumpInputStartTime = Time.time;
		}
		else if (context.canceled)
		{
			_jumpController.Jump = false;
		}
	}
}
