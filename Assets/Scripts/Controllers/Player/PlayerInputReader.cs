using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MoveController), typeof(JumpController), typeof(DashController))]
[RequireComponent(typeof(GrabController), typeof(AttackController), typeof(AbilityController))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerInputReader : Component, IMoveController, IJumpController, IGrabController, IAttackController, IDashContorller, IAbilityControlller
{
    [SerializeField] private float _jumpInputHoldTime;
    [SerializeField] private float _dashInputPressTime;

    private Vector2 _mouseInputPosition;

    private Camera _camera;
    private PlayerInput _playerInput;

    private float _jumpInputStartTime;
    private float _dashInputStartTime;

    private MoveController _moveController;
    private JumpController _jumpController;
    private DashController _dashController;
    private GrabController _grabController;
    private AttackController _attackController;
    private AbilityController _abilityController;

    public Vector2Int Move
    {
        get => _moveController.Move;
        set => _moveController.Move = value;
    }

    public Vector2 LookAt
    {
        get => _moveController.LookAt;
        set => _moveController.LookAt = value;
    }

    public bool Jump
    {
        get => _jumpController.Jump;
        set => _jumpController.Jump = value;
    }

    public bool Grab
    {
        get => _grabController.Grab;
        set => _grabController.Grab = value;
    }

    public bool Dash
    {
        get => _dashController.Dash;
        set => _dashController.Dash = value;
    }
    public bool Attack
    {
        get => _attackController.Attack;
        set => _attackController.Attack = value;
    }
    public bool Ability
    {
        get => _abilityController.Ability;
        set => _abilityController.Ability = value;
    }

    private void Awake()
    {
        _camera = Camera.main;
        _playerInput = GetComponent<PlayerInput>();
        _moveController = GetComponent<MoveController>();
        _jumpController = GetComponent<JumpController>();
        _dashController = GetComponent<DashController>();
        _grabController = GetComponent<GrabController>();
        _attackController = GetComponent<AttackController>();
        _abilityController = GetComponent<AbilityController>();
    }

    private void Update()
    {
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
