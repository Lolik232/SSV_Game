using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using All.Events;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngineInternal;

[RequireComponent(typeof(StateMachine), typeof(Rigidbody2D), typeof(Animator))]

public class Player : MonoBehaviour
{
    private BoxCollider2D _collider;
    private Rigidbody2D _rb;
    private StateMachine _machine;
    private Animator _animator;
    private TrailRenderer _tr;

    [Header("States")]
    [SerializeField] private List<PlayerStateSO> _states = new();

    [Header("Abilities")]
    [SerializeField] private List<PlayerAbilitySO> _abilities = new();

    [Header("Checkers")]
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private Transform _ceilingChecker;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private Transform _wallChecker;
    [SerializeField] private Transform _ledgeChecker;
    [SerializeField] private float _wallCheckDistance;
    [SerializeField] private List<LayerMask> _whatIsTarget;

    [Header("Input")]
    [SerializeField] private PlayerInputReaderSO _inputReader;
    [Header("Jump Input")]
    [SerializeField] private float _jumpInputHoldTime;

    [Header("Parameters")]
    [SerializeField] private Vector2 _standOffset;
    [SerializeField] private Vector2 _standSize;
    public Vector2 StandSize => _standSize;

    [SerializeField] private Vector2 _crouchOffset;
    [SerializeField] private Vector2 _crouchSize;
    public Vector2 CrouchSize => _crouchSize;

    [Header("Movement")]
    [SerializeField] private int _moveSpeed;
    public int MoveSpeed => _moveSpeed;

    [SerializeField] private int _crouchMoveSpeed;
    public int CrouchMoveSpeed => _crouchMoveSpeed;

    [SerializeField] private int _inAirMoveSpeed;
    public int InAirMoveSpeed => _inAirMoveSpeed;

    [Header("Touching Wall")]
    [SerializeField] private int _wallSlideSpeed;
    public int WallSlideSpeed => _wallSlideSpeed;

    [SerializeField] private int _wallClimbSpeed;
    public int WallClimbSpeed => _wallClimbSpeed;

    [Header("On Ledge")]
    [SerializeField] private Vector2 _startLedgeOffset;
    public Vector2 StartLedgeOffset => _startLedgeOffset;

    [SerializeField] private Vector2 _endLedgeOffset;
    public Vector2 EndLedgeOffset => _endLedgeOffset;

    public Vector2 Velocity => _rb.velocity;
    public Vector2 Position => transform.position;
    public Vector2 Size => _collider.size;
    public Vector2 Offset => _collider.offset;

    [NonSerialized] public int facingDirection = 1;
    [NonSerialized] public int wallDirection;

    [NonSerialized] public Vector2Int moveInput;

    private float _jumpInputStartTime;
    [NonSerialized] public bool jumpInput;
    [NonSerialized] public bool jumpInputHold;

    [NonSerialized] public bool grabInput;

    [NonSerialized] public bool dashInput;
    [NonSerialized] public Vector2 dashDirection;

    [NonSerialized] public bool abilityInput;
    [NonSerialized] public Vector2 abilityDirection;

    [NonSerialized] public bool isGrounded;
    [NonSerialized] public bool isGroundClose;
    [NonSerialized] public bool isTouchingCeiling;
    [NonSerialized] public bool isTouchingWall;
    [NonSerialized] public bool isTouchingWallBack;
    [NonSerialized] public bool isTouchingLedge;

    [NonSerialized] public Vector2 wallPosition;
    [NonSerialized] public Vector2 cornerPosition;
    [NonSerialized] public Vector2 ledgeStartPosition;
    [NonSerialized] public Vector2 ledgeEndPosition;

    private void OnMove(Vector2Int value) => moveInput = value;
    private void OnJump()
    {
        jumpInput = true;
        jumpInputHold = true;
        _jumpInputStartTime = Time.time;
    }
    private void OnJumpCanceled() => jumpInputHold = false;
    private void OnGrab() => grabInput = true;
    private void OnGrabCanceled() => grabInput = false;

    private void OnDash(Vector2 direction)
    {
        dashInput = true;
        dashDirection = (direction - (Vector2)transform.position).normalized;
    }
    private void OnDashCanceled() => dashInput = false;

    private void OnAbility(Vector2 direction)
    {
        abilityInput = true;
        abilityDirection = (direction - (Vector2)transform.position).normalized;
    }
    private void OnAbilityCanceled() => abilityInput = false;

    private void CheckJumpInputHoldTime() => jumpInput &= Time.time < _jumpInputStartTime + _jumpInputHoldTime;

    public void CheckIfGrounded()
    {
        foreach (var target in _whatIsTarget)
        {
            isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, target);
            return;
        }
    }
    public void CheckIfGroundClose()
    {
        foreach (var target in _whatIsTarget)
        {
            isGroundClose = Physics2D.Raycast(_groundChecker.position, Vector2.down, _groundCheckDistance, target);
            return;
        }
    }

    public void CheckIfTouchingCeiling()
    {
        foreach (var target in _whatIsTarget)
        {
            isTouchingCeiling = Physics2D.OverlapCircle(_ceilingChecker.position, _groundCheckRadius, target);
            return;
        }
    }
    public void CheckIfTouchingWall()
    {
        foreach (var target in _whatIsTarget)
        {
            RaycastHit2D hit = Physics2D.Raycast(_wallChecker.position, facingDirection * Vector2.right, _wallCheckDistance, target);
            if (hit) { wallPosition = new Vector2(_wallChecker.position.x + facingDirection * hit.distance, _wallChecker.position.y); }
            wallDirection = hit ? -facingDirection : facingDirection;
            isTouchingWall = hit;
            return;
        }
    }
    public void CheckIfTouchingWallBack()
    {
        foreach (var target in _whatIsTarget)
        {
            isTouchingWallBack = Physics2D.Raycast(_wallChecker.position, facingDirection * Vector2.left, _wallCheckDistance, target);
            return;
        }
    }
    public void CheckIfTouchingLedge()
    {
        foreach (var target in _whatIsTarget)
        {
            isTouchingLedge = Physics2D.Raycast(_ledgeChecker.position, facingDirection * Vector2.right, _wallCheckDistance, target);
            return;
        }
    }

    public void DeterminCornerPosition()
    {
        foreach (var target in _whatIsTarget)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(wallPosition.x + 0.01f * facingDirection, _ledgeChecker.position.y), Vector2.down, _ledgeChecker.position.y - _wallChecker.position.y, target);
            if (hit)
            {
                cornerPosition = new Vector2(wallPosition.x, _ledgeChecker.position.y - hit.distance);
                return;
            }

        }
    }

    public void CheckIfTouchingCeilingWhenClimb()
    {
        foreach (var target in _whatIsTarget)
        {
            isTouchingCeiling = Physics2D.Raycast(ledgeEndPosition, Vector2.up, _standSize.y, target);
            return;
        }
    }

    public void CheckIfShouldFlip(int direction)
    {
        if (facingDirection != direction && direction != 0)
        {
            facingDirection = -facingDirection;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction) => _rb.velocity = new Vector2(angle.normalized.x * velocity * direction, angle.normalized.y * velocity);
    public void SetVelocityX(float xVelocity) => _rb.velocity = new Vector2(xVelocity, _rb.velocity.y);
    public void SetVelocityY(float yVelocity) => _rb.velocity = new Vector2(_rb.velocity.x, yVelocity);
    public void SetVelocity(Vector2 veloity) => _rb.velocity = veloity;
    public void SetVelocityZero() => _rb.velocity = Vector2.zero;

    public void MoveToX(float x) => transform.position = new Vector2(x, transform.position.y);
    public void MoveToY(float y) => transform.position = new Vector2(transform.position.x, y);
    public void HoldPosition(Vector2 holdPosition)
    {
        transform.position = holdPosition;
        _rb.velocity = Vector2.zero;
    }

    public void Stand()
    {
        _collider.size = _standSize;
        _collider.offset = _standOffset;
    }

    public void Crouch()
    {
        _collider.size = _crouchSize;
        _collider.offset = _crouchOffset;
    }

    public void EnableTrail() => _tr.emitting = true;
    public void DisableTrail() => _tr.emitting = false;

    private void Awake()
    {
        _tr = GetComponent<TrailRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _machine = GetComponent<StateMachine>();
        _animator = GetComponent<Animator>();
        _inputReader.InitializePlayerInput(GetComponent<PlayerInput>());
    }

    private void Start()
    {
        Stand();
        facingDirection = 1;

        _inputReader.MoveEvent += OnMove;
        _inputReader.JumpEvent += OnJump;
        _inputReader.JumpCanceledEvent += OnJumpCanceled;
        _inputReader.GrabEvent += OnGrab;
        _inputReader.GrabCanceledEvent += OnGrabCanceled;
        _inputReader.DashEvent += OnDash;
        _inputReader.DashCanceledEvent += OnDashCanceled;
        _inputReader.AbilityEvent += OnAbility;
        _inputReader.AbilityCanceledEvent += OnAbilityCanceled;

        foreach (var state in _states) { state.Initialize(this, _machine, _animator); }
        foreach (var ability in _abilities) { ability.Initialize(this, _machine); }
    }

    private void OnDestroy()
    {
        _inputReader.MoveEvent -= OnMove;
        _inputReader.JumpEvent -= OnJump;
        _inputReader.JumpCanceledEvent -= OnJumpCanceled;
        _inputReader.GrabEvent -= OnGrab;
        _inputReader.GrabCanceledEvent -= OnGrabCanceled;
        _inputReader.DashEvent -= OnDash;
        _inputReader.DashCanceledEvent -= OnDashCanceled;
        _inputReader.AbilityEvent -= OnAbility;
        _inputReader.AbilityCanceledEvent -= OnAbilityCanceled;
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        foreach (var ability in _abilities)
        {
            ability.TryUseAbility();
            ability.OnUpdate();
        }
    }

    private void OnDrawGizmos()
    {
        if (_collider != null)
        {
            Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y + _collider.offset.y), _collider.size);
            Gizmos.DrawWireCube(new Vector2(ledgeEndPosition.x, ledgeEndPosition.y + _collider.offset.y), _collider.size);

            Gizmos.DrawLine(ledgeEndPosition, new Vector2(ledgeEndPosition.x, ledgeEndPosition.y + _standSize.y));
            Gizmos.DrawWireCube(new Vector2(ledgeStartPosition.x, ledgeStartPosition.y + _collider.offset.y), _collider.size);
        }

        Gizmos.DrawWireSphere(_groundChecker.position, _groundCheckRadius);
        Gizmos.DrawWireSphere(_ceilingChecker.position, _groundCheckRadius);
        Gizmos.DrawLine(_groundChecker.position, new Vector2(_groundChecker.position.x, _groundChecker.position.y - _groundCheckDistance));
        Gizmos.DrawLine(_wallChecker.position, new Vector2(_wallChecker.position.x + facingDirection * _wallCheckDistance, _wallChecker.position.y));
        Gizmos.DrawLine(_wallChecker.position, new Vector2(_wallChecker.position.x - facingDirection * _wallCheckDistance, _wallChecker.position.y));
        Gizmos.DrawLine(_ledgeChecker.position, new Vector2(_ledgeChecker.position.x + facingDirection * _wallCheckDistance, _ledgeChecker.position.y));
    }
}
