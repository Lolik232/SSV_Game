using System;
using System.Collections;
using System.Collections.Generic;
using All.Events;

using UnityEngine;

[RequireComponent(typeof(StateMachine))]

public class Player : MonoBehaviour
{
    [SerializeField] private SuperStateSO _groundedState;
    [SerializeField] private SuperStateSO _touchingWallState;
    [SerializeField] private StateSO _inAirState;
    [SerializeField] private StateSO _ledgeClimbState;

    [SerializeField] private BoolEventChannelSO _groundedEventWriter;
    [SerializeField] private BoolEventChannelSO _groundFarEventWriter;
    [SerializeField] private BoolEventChannelSO _touchingWallEventWriter;
    [SerializeField] private BoolEventChannelSO _touchingWallBackEventWriter;
    [SerializeField] private BoolEventChannelSO _touchingLedgeEventWriter;

    private Rigidbody2D _rb;

    [SerializeField] private IntEventChannelSO _xMoveListener;

    private int _facingDirection;

    private void CheckIfShouldFlip(int direction)
    {
        if (_facingDirection != direction && direction != 0)
        {
            _facingDirection = -_facingDirection;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void OnXMove(int direction)
    {
        CheckIfShouldFlip(direction);
        SetVelocityX(direction * 10);
    }

    private void SetVelocityX(int xVelocity)
    {
        var workspace = new Vector2(xVelocity, _rb.velocity.y);
        _rb.velocity = workspace;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _facingDirection = 1;
        _xMoveListener.OnEventRaised += OnXMove;
    }

    private void OnDestroy()
    {
        _xMoveListener.OnEventRaised -= OnXMove;

    }

    private void Update()
    {
        _groundedState.OnUpdate();
    }

    private void FixedUpdate()
    {
        
    }
}
