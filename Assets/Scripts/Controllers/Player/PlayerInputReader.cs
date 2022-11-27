using System;

using All.Events;

using Input;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

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
                                 IAbilityControlller,
                                 IBlockableBySpell,
                                 IBlockable
{
    [SerializeField] private SpriteRenderer _stan;

    public AbilitySO description;

    private GameInput _gameInput;

    private Inventory _inventory;

    private Blocker _blocker = new();

    [SerializeField] private VoidEventChannelSO _pauseEventChannel = default;

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
        get => ((IMoveController)_moveController).Move;
        set => ((IMoveController)_moveController).Move = value;
    }

    public Vector2 LookAt
    {
        get => ((IMoveController)_moveController).LookAt;
        set => ((IMoveController)_moveController).LookAt = value;
    }

    public bool Jump
    {
        get => ((IJumpController)_jumpController).Jump;
        set => ((IJumpController)_jumpController).Jump = value;
    }

    public bool Grab
    {
        get => ((IGrabController)_grabController).Grab;
        set => ((IGrabController)_grabController).Grab = value;
    }

    public bool Attack
    {
        get => ((IAttackController)_attackController).Attack;
        set => ((IAttackController)_attackController).Attack = value;
    }

    public bool Dash
    {
        get => ((IDashContorller)_dashController).Dash;
        set => ((IDashContorller)_dashController).Dash = value;
    }

    public bool Ability
    {
        get => ((IAbilityControlller)_abilityController).Ability;
        set => ((IAbilityControlller)_abilityController).Ability = value;
    }
    public bool IsLocked
    {
        get => _blocker.IsLocked;
    }
    public AbilitySO Description
    {
        get => description;
    }

    private void Awake()
    {
        _inventory = GetComponentInChildren<Inventory>();
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
        if (_playerInput.currentControlScheme == "Keyboard")
        {
            _moveController.LookAt = _camera.ScreenToWorldPoint(_mouseInputPosition);
        }
        else
        {
            _moveController.LookAt = (Vector2)transform.position + _mouseInputPosition * 10f;
        }
        
        // _moveController.LookAt = _camera.ScreenToWorldPoint(_mouseInputPosition);
        _jumpController.Jump &= Time.time < _jumpInputStartTime + _jumpInputHoldTime;
        _dashController.Dash &= Time.time < _dashInputStartTime + _dashInputPressTime;
    }

    private void OnEnable()
    {
        if (_gameInput == null)
        {
            _gameInput = GameInputSingleton.GameInput;
        }

        _gameInput.Gameplay.SetCallbacks(this);
    }

    private void Start()
    {
        _stan.enabled = false;
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
            _jumpInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            _jumpController.Jump = false;
        }
    }

    public void OnGrab(InputAction.CallbackContext context)
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

    public void OnDash(InputAction.CallbackContext context)
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

    public void OnAttack(InputAction.CallbackContext context)
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

    public void OnAbility(InputAction.CallbackContext context)
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

    public void OnDirection(InputAction.CallbackContext context)
    {
        _mouseInputPosition = context.ReadValue<Vector2>();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _pauseEventChannel.RaiseEvent();
        }
    }

    public void OnChangeWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _inventory.GetNext();
        }
    }

    public void Block()
    {
        _stan.enabled = true;
        GameInputSingeltone.GameInput.DisablePlayerInput();
        _blocker.AddBlock();
        Move = Vector2Int.zero;
    }

    public void Unlock()
    {
        _stan.enabled = false;
        _blocker.RemoveBlock();
        if (!IsLocked)
        {
            GameInputSingeltone.GameInput.EnablePlayerInput();
        }
    }
}