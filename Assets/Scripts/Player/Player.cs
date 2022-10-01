using System;
using System.Collections;
using System.Collections.Generic;

using All.Events;

using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngineInternal;

[RequireComponent(typeof(StateMachine), typeof(Rigidbody2D), typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private List<PlayerStateSO> _states = new();

    private Rigidbody2D _rb;
    private StateMachine _machine;
    private Animator _animator;
    [SerializeField] private PlayerInputReader _inputReader;

    [SerializeField] private Transform _groundChecker;
    [SerializeField] private Transform _wallChecker;
    [SerializeField] private Transform _ledgeChecker;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private float _wallCheckDistance;
    [SerializeField] private LayerMask _whatIsGround;

    [SerializeField] private int _moveSpeed;
    public int MoveSpeed => _moveSpeed;
    [SerializeField] private Vector2 _rbSize;
    public Vector2 RbSize => _rbSize;
    [SerializeField] private int _inAirMoveSpeed;
    public int InAirMoveSpeed => _inAirMoveSpeed;
    [SerializeField] private int _jumpForce;
    public int JumpForce => _jumpForce;
    [SerializeField] private float _coyoteTime;
    public float CoyoteTime => _coyoteTime;
    [SerializeField] private int _wallJumpForce;
    public int WallJumpForce => _wallJumpForce;
    [SerializeField] private Vector2 _wallJumpAngle;
    public Vector2 WallJumpAngle => _wallJumpAngle;
    [SerializeField] private float _wallJumpTime;
    public float WallJumpTime => _wallJumpTime;
    [SerializeField] private float _jumpInputHoldTime;
    [SerializeField] private int _wallSlideSpeed;
    public int WallSlideSpeed => _wallSlideSpeed;
    [SerializeField] private int _wallClimbSpeed;
    public int WallClimbSpeed => _wallClimbSpeed;
    public Vector2 Velocity => _rb.velocity;

    [NonSerialized] public int facingDirection = 1;
    [NonSerialized] public int wallDirection;
    [NonSerialized] public Vector2Int moveInput;

    [NonSerialized] public bool jumpInput;
    [NonSerialized] public bool jumpInputHold;
    [NonSerialized] public bool jumpCoyoteTime;
    [NonSerialized] public bool wallJump;
    [NonSerialized] public bool wallJumpCoyoteTime;
    [NonSerialized] private float _jumpStartTime;

    [NonSerialized] public bool grabInput;

    [NonSerialized] public bool isGrounded;
    [NonSerialized] public bool isGroundClose;
    [NonSerialized] public bool isTouchingWall;
    [NonSerialized] public bool isTouchingWallBack;
    [NonSerialized] public bool isTouchingLedge;

    [NonSerialized] public Vector2 wallPosition;
    [NonSerialized] public Vector2 cornerPosition;

    private void OnMove(Vector2Int value) => moveInput = value;
    private void OnJump()
    {
        jumpInput = true;
        jumpInputHold = true;
        _jumpStartTime = Time.time;
    }
    private void OnJumpCanceled() => jumpInputHold = false;
    private void CheckJumpInputHoldTime()
    {
        jumpInput = jumpInput && Time.time < _jumpStartTime + _jumpInputHoldTime;
    }
    private void CheckWallJumpTime()
    {
        wallJump = wallJump && Time.time < _jumpStartTime + _wallJumpTime;
    }
    private void CheckCoyoteTime()
    {
        jumpCoyoteTime = jumpCoyoteTime && Time.time < _jumpStartTime + _coyoteTime;
        wallJumpCoyoteTime = wallJumpCoyoteTime && Time.time < _jumpStartTime + _coyoteTime;
    }
    private void OnGrab() => grabInput = true;
    private void OnGrabCanceled() => grabInput = false;

    private void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, _whatIsGround);
    }
    private void CheckIfGroundClose() => isGroundClose = Physics2D.Raycast(_groundChecker.position, Vector2.down, _groundCheckDistance, _whatIsGround);
    private void CheckIfTouchingWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(_wallChecker.position, facingDirection * Vector2.right, _wallCheckDistance, _whatIsGround);
        if (hit) { wallPosition = new Vector2(_wallChecker.position.x + facingDirection * hit.distance, _wallChecker.position.y); }
        wallDirection = hit ? -facingDirection : facingDirection;
        isTouchingWall = hit;
    }
    private void CheckIfTouchingWallBack()
    {
        isTouchingWallBack = Physics2D.Raycast(_wallChecker.position, facingDirection * Vector2.left, _wallCheckDistance, _whatIsGround);
    }
    private void CheckIfTouchingLedge()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(wallPosition.x, _ledgeChecker.position.y), Vector2.down, _ledgeChecker.position.y - _wallChecker.position.y, _whatIsGround);
        if (hit) { cornerPosition = new Vector2(wallPosition.x, _ledgeChecker.position.y - hit.distance); }
        isTouchingLedge = hit;
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
    public void SetVelocityZero() => _rb.velocity = Vector2.zero;

    public void MoveToX(float x) => transform.position = new Vector2(x, transform.position.y);
    public void MoveToY(float y) => transform.position = new Vector2(transform.position.x, y);
    public void HoldPosition(Vector2 holdPosition)
    {
        transform.position = holdPosition;
        _rb.velocity = Vector2.zero;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _machine = GetComponent<StateMachine>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        facingDirection = 1;

        _inputReader.MoveEvent += OnMove;
        _inputReader.JumpEvent += OnJump;
        _inputReader.JumpCanceledEvent += OnJumpCanceled;
        _inputReader.GrabEvent += OnGrab;
        _inputReader.GrabCanceledEvent += OnGrabCanceled;

        foreach (var state in _states) { state.Initialize(this, _machine, _animator); }
    }

    private void OnDestroy()
    {
        _inputReader.MoveEvent -= OnMove;
        _inputReader.JumpEvent -= OnJump;
        _inputReader.JumpCanceledEvent -= OnJumpCanceled;
        _inputReader.GrabEvent -= OnGrab;
        _inputReader.GrabCanceledEvent -= OnGrabCanceled;
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckWallJumpTime();
        CheckCoyoteTime();
    }

    private void FixedUpdate()
    {
        CheckIfGrounded();
        CheckIfGroundClose();
        CheckIfTouchingWall();
        CheckIfTouchingWallBack();
        CheckIfTouchingLedge();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundChecker.position, _groundCheckRadius);
        Gizmos.DrawLine(_groundChecker.position, new Vector2(_groundChecker.position.x, _groundChecker.position.y - _groundCheckDistance));
        Gizmos.DrawLine(_wallChecker.position, new Vector2(_wallChecker.position.x + facingDirection * _wallCheckDistance, _wallChecker.position.y));
        Gizmos.DrawLine(_wallChecker.position, new Vector2(_wallChecker.position.x - facingDirection * _wallCheckDistance, _wallChecker.position.y));
        Gizmos.DrawLine(_ledgeChecker.position, new Vector2(_ledgeChecker.position.x + facingDirection * _wallCheckDistance, _ledgeChecker.position.y));
    }
}
