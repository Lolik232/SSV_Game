using System;
using All.Events;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MoveController), typeof(JumpController), typeof(DashController))]
[RequireComponent(typeof(GrabController), typeof(AttackController), typeof(AbilityController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerInputReader : Component,
                                 GameInput.IGameplayActions,
                                 IMoveController,
                                 IJumpController,
                                 IGrabController,
                                 IAttackController,
                                 IDashContorller,
                                 IAbilityControlller
{
    private GameInput _gameInput;

    [SerializeField] private VoidEventChannelSO _pauseEventChannel = default;

    [SerializeField] private float _jumpInputHoldTime;
    [SerializeField] private float _dashInputPressTime;

    private Vector2 _mouseInputPosition;

    private Camera      _camera;
    private PlayerInput _playerInput;

    private float _jumpInputStartTime;
    private float _dashInputStartTime;

    private MoveController    _moveController;
    private JumpController    _jumpController;
    private DashController    _dashController;
    private GrabController    _grabController;
    private AttackController  _attackController;
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
        _camera            = Camera.main;
        _playerInput       = GetComponent<PlayerInput>();
        _moveController    = GetComponent<MoveController>();
        _jumpController    = GetComponent<JumpController>();
        _dashController    = GetComponent<DashController>();
        _grabController    = GetComponent<GrabController>();
        _attackController  = GetComponent<AttackController>();
        _abilityController = GetComponent<AbilityController>();
    }

    private void Update()
    {
        _moveController.LookAt =  _camera.ScreenToWorldPoint(_mouseInputPosition);
        _jumpController.Jump   &= Time.time < _jumpInputStartTime + _jumpInputHoldTime;
        _dashController.Dash   &= Time.time < _dashInputStartTime + _dashInputPressTime;
    }

    private void OnEnable()
    {
        if (_gameInput == null)
        {
            _gameInput = GameInputSingeltone.GameInput;
            _gameInput.Gameplay.SetCallbacks(this);
        }
    }


    public void OnMovement(InputAction.CallbackContext context)
    {
        _moveController.Move = Vector2Int.RoundToInt(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _jumpController.Jump = true;
            _jumpInputStartTime  = Time.time;
        } else if (context.canceled)
        {
            _jumpController.Jump = false;
        }
    }

    public void OnGrab(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _grabController.Grab = true;
        } else if (context.canceled)
        {
            _grabController.Grab = false;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _dashController.Dash = true;
            _dashInputStartTime  = Time.time;
        } else if (context.canceled)
        {
            _dashController.Dash = false;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _attackController.Attack = true;
        } else if (context.canceled)
        {
            _attackController.Attack = false;
        }
    }

    public void OnAbility(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _abilityController.Ability = true;
        } else if (context.canceled)
        {
            _abilityController.Ability = false;
        }
    }

    public void OnDirection(InputAction.CallbackContext context)
    {
        if (_playerInput.currentControlScheme == "Keyboard")
        {
            _mouseInputPosition = context.ReadValue<Vector2>();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed) _pauseEventChannel.RaiseEvent();
    }
}