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

    [Header("Checkers")]
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private Transform _ceilingChecker;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private Transform _wallChecker;
    [SerializeField] private Transform _ledgeChecker;
    [SerializeField] private float _wallCheckDistance;
    [SerializeField] private LayerMask _whatIsGround;

    [Header("Input")]
    [SerializeField] private PlayerInputReaderSO _inputReader;
    [Header("Jump Input")]
    [SerializeField] private float _jumpInputHoldTime;

    [Header("Parameters")]


    [Header("Movement")]
    [SerializeField] private int _moveSpeed;
    public int MoveSpeed => _moveSpeed;

    [SerializeField] private int _crouchMoveSpeed;
    public int CrouchMoveSpeed => _crouchMoveSpeed;

    [SerializeField] private int _inAirMoveSpeed;
    public int InAirMoveSpeed => _inAirMoveSpeed;

    [Header("Jump")]
    [SerializeField] private int _jumpForce;
    public int JumpForce => _jumpForce;

    [SerializeField] private float _coyoteTime;

    [Header("Wall Jump")]
    [SerializeField] private int _wallJumpForce;
    public int WallJumpForce => _wallJumpForce;

    [SerializeField] private Vector2 _wallJumpAngle;
    public Vector2 WallJumpAngle => _wallJumpAngle;

    [SerializeField] private float _wallJumpTime;
    public float WallJumpTime => _wallJumpTime;

    [Header("Dash")]
    [SerializeField] private int _dashForce;
    public int DashForce => _dashForce;

    [SerializeField] private float _dashTime;
    public float DashTime => _dashTime;

    [SerializeField] private float _dashCooldown;
    public float DashCooldown => _dashCooldown;

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

    [Header("Crouch")]
    [SerializeField] private Vector2 _standOffset;
    [SerializeField] private Vector2 _standSize;
    public Vector2 StandSize => _standSize;

    [SerializeField] private Vector2 _crouchOffset;
    [SerializeField] private Vector2 _crouchSize;
    public Vector2 CrouchSize => _crouchSize;

    public Vector2 Velocity => _rb.velocity;
    public Vector2 Size => _collider.size;
    public Vector2 Offset => _collider.offset;

    [NonSerialized] public int facingDirection = 1;
    [NonSerialized] public int wallDirection;
    [NonSerialized] public Vector2Int moveInput;
    [NonSerialized] public Vector2 dashDirection;

    private float _jumpInputStartTime;

    [NonSerialized] public bool jumpInput;
    [NonSerialized] public bool jumpInputHold;
    [NonSerialized] public bool jumpCoyoteTime;
    [NonSerialized] public bool jump;
    [NonSerialized] public bool wallJump;
    [NonSerialized] public bool dashInput;
    [NonSerialized] public bool dash;
    [NonSerialized] public bool canDash;
    [NonSerialized] public bool wallJumpCoyoteTime;
    [NonSerialized] public float jumpStartTime;
    [NonSerialized] public float coyoteTimeStart;
    [NonSerialized] public float dashStartTime;

    [NonSerialized] public bool grabInput;

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
    private void CheckJumpInputHoldTime()
    {
        jumpInput = jumpInput && Time.time < _jumpInputStartTime + _jumpInputHoldTime;
    }
    private void CheckWallJumpTime()
    {
        wallJump = wallJump && Time.time < jumpStartTime + _wallJumpTime;
    }
    private void CheckDashTime()
    {
        dash = dash && Time.time < dashStartTime + _dashTime;
    }
    private void CheckDashCooldown()
    {
        canDash = Time.time >= dashStartTime + _dashTime + _dashCooldown;
    }
    private void CheckCoyoteTime()
    {
        jumpCoyoteTime = jumpCoyoteTime && Time.time < coyoteTimeStart + _coyoteTime;
        wallJumpCoyoteTime = wallJumpCoyoteTime && Time.time < coyoteTimeStart + _coyoteTime;
    }

    public void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, _whatIsGround);
    }
    public void CheckIfGroundClose()
    {
        isGroundClose = Physics2D.Raycast(_groundChecker.position, Vector2.down, _groundCheckDistance, _whatIsGround);
    }

    public void CheckIfTouchingCeiling()
    {
        isTouchingCeiling = Physics2D.OverlapCircle(_ceilingChecker.position, _groundCheckRadius, _whatIsGround);
    }
    public void CheckIfTouchingWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(_wallChecker.position, facingDirection * Vector2.right, _wallCheckDistance, _whatIsGround);
        if (hit) { wallPosition = new Vector2(_wallChecker.position.x + facingDirection * hit.distance, _wallChecker.position.y); }
        wallDirection = hit ? -facingDirection : facingDirection;
        isTouchingWall = hit;
    }
    public void CheckIfTouchingWallBack()
    {
        isTouchingWallBack = Physics2D.Raycast(_wallChecker.position, facingDirection * Vector2.left, _wallCheckDistance, _whatIsGround);
    }
    public void CheckIfTouchingLedge()
    {
        isTouchingLedge = Physics2D.Raycast(_ledgeChecker.position, facingDirection * Vector2.right, _wallCheckDistance, _whatIsGround);
    }

    public void DeterminCornerPosition()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(wallPosition.x + 0.01f * facingDirection, _ledgeChecker.position.y), Vector2.down, _ledgeChecker.position.y - _wallChecker.position.y, _whatIsGround);
        if (hit) { cornerPosition = new Vector2(wallPosition.x, _ledgeChecker.position.y - hit.distance); }
    }

    public void CheckIfTouchingCeilingWhenClimb()
    {
        isTouchingCeiling = Physics2D.Raycast(ledgeEndPosition, Vector2.up, _standSize.y, _whatIsGround);
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

    public void SetColiderStandSize()
    {
        _collider.size = _standSize;
        _collider.offset = _standOffset;
    }

    public void SetColiderCrouchSize()
    {
        _collider.size = _crouchSize;
        _collider.offset = _crouchOffset;
    }

    public void EnableTrail()
    {
        _tr.emitting = true;
    }

    public void DisableTrail()
    {
        _tr.emitting = false;
    }

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
        facingDirection = 1;

        SetColiderStandSize();

        _inputReader.MoveEvent += OnMove;
        _inputReader.JumpEvent += OnJump;
        _inputReader.JumpCanceledEvent += OnJumpCanceled;
        _inputReader.GrabEvent += OnGrab;
        _inputReader.GrabCanceledEvent += OnGrabCanceled;
        _inputReader.DashEvent += OnDash;
        _inputReader.DashCanceledEvent += OnDashCanceled;

        foreach (var state in _states) { state.Initialize(this, _machine, _animator); }
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
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckWallJumpTime();
        CheckDashTime();
        CheckDashCooldown();
        CheckCoyoteTime();
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
