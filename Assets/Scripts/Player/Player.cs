using System;
using System.Collections;
using System.Collections.Generic;
using All.Events;

using UnityEngine;

[RequireComponent(typeof(StateMachine), typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerGroundedSuperStateSO _groundedState;
    [SerializeField] private SuperStateSO _touchingWallState;
    [SerializeField] private PlayerInAirStateSO _inAirState;
    [SerializeField] private StateSO _ledgeClimbState;

    private Rigidbody2D _rb;
    private StateMachine _machine;
    [SerializeField] private PlayerInputReader _inputReader;

    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _whatIsGround;

    private int _facingDirection;

    private Vector2Int _moveInput;
    public Vector2Int MoveInput => _moveInput;

    [SerializeField] private int _moveSpeed;
    public int MoveSpeed => _moveSpeed;

    [SerializeField] private int _inAirMoveSpeed;
    public int InAirMoveSpeed => _inAirMoveSpeed;

    private void OnMove(Vector2Int value) => _moveInput = value;

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, _whatIsGround);

    }

    public void CheckIfShouldFlip(int direction)
    {
        if (_facingDirection != direction && direction != 0)
        {
            _facingDirection = -_facingDirection;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    public void SetVelocityX(float xVelocity)
    {
        var workspace = new Vector2(xVelocity, _rb.velocity.y);
        _rb.velocity = workspace;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _machine = GetComponent<StateMachine>();
    }

    private void Start()
    {
        _facingDirection = 1;

        _inputReader.MoveEvent += OnMove;

        _groundedState.InitializeStateMachine(_machine);
        _inAirState.InitializeStateMachine(_machine);
        _groundedState.InitializePlayer(this);
        _inAirState.InitializePlayer(this);
    }

    private void OnDestroy()
    {
        _inputReader.MoveEvent -= OnMove;
    }

    private void Update()
    {
        _groundedState.OnUpdate();
    }

    private void FixedUpdate()
    {
        _groundedState.OnFixedUpdate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundChecker.position, _groundCheckRadius);
    }
}
