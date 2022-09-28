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
    [SerializeField] private List<PlayerSuperStateSO> _superStates;
    [SerializeField] private List<PlayerSubStateSO> _subStates;
    [SerializeField] private List<PlayerStateSO> _states;

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

    [SerializeField] private int _inAirMoveSpeed;
    public int InAirMoveSpeed => _inAirMoveSpeed;

    [SerializeField] private int _jumpForce;
    public int JumpForce => _jumpForce;

    [SerializeField] private float _jumpInputHoldTime;

    [SerializeField] private int _wallSlideSpeed;
    public int WallSlideSpeed => _wallSlideSpeed;

    public Vector2 Velocity => _rb.velocity;

    [NonSerialized] public int facingDirection = 1;
    [NonSerialized] public Vector2Int moveInput;

    [NonSerialized] public bool jumpInput;
    [NonSerialized] public bool jumpInputHold;
    [NonSerialized] private float _jumpStarTime;

    [NonSerialized] public bool grabInput;

    private void OnMove(Vector2Int value) => moveInput = value;
    private void OnJump()
    {
        jumpInput = true;
        jumpInputHold = true;
        _jumpStarTime = Time.time;
    }
    private void OnJumpCanceled() => jumpInputHold = false;
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= _jumpStarTime + _jumpInputHoldTime) { jumpInput = false; }
    }
    private void OnGrab() => grabInput = true;
    private void OnGrabCanceled() => grabInput = false;

    public bool CheckIfGrounded() => Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, _whatIsGround);
    public bool CheckIfGroundClose() => Physics2D.Raycast(_groundChecker.position, Vector2.down, _groundCheckDistance, _whatIsGround);
    public bool CheckIfTouchingWall() => Physics2D.Raycast(_wallChecker.position, facingDirection * Vector2.right, _wallCheckDistance, _whatIsGround);
    public bool CheckIfTouchingWallBack() => Physics2D.Raycast(_wallChecker.position, facingDirection * Vector2.left, _wallCheckDistance, _whatIsGround);
    public bool CheckIfTouchingLedge() => Physics2D.Raycast(_ledgeChecker.position, facingDirection * Vector2.right, _wallCheckDistance, _whatIsGround);

    public void CheckIfShouldFlip(int direction)
    {
        if (facingDirection != direction && direction != 0)
        {
            facingDirection = -facingDirection;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    public void SetVelocityX(float xVelocity) => _rb.velocity = new Vector2(xVelocity, _rb.velocity.y);

    public void SetVelocityY(float yVelocity) => _rb.velocity = new Vector2(_rb.velocity.x, yVelocity);

    public void SetVelocityZero() => _rb.velocity = Vector2.zero;

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
        foreach (var state in _subStates) { state.Initialize(this, _machine, _animator); }
        foreach (var superState in _superStates) { superState.Initialize(this, _machine); }
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
