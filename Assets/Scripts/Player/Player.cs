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
    private bool _isVelocityHeld;
    private Vector2 _cachedVelocity;

    [SerializeField] private GameObject _weaponBase;
    public Weapon Weapon { get; private set; }
    public BoxCollider2D Collider { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public StateMachine Machine { get; private set; }
    public Animator Anim { get; private set; }
    public TrailRenderer Tr { get; private set; }

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
    [SerializeField] private float _wallCloseCheckDistance;
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

    public Vector2 Center => (Vector2)transform.position + Collider.offset;

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

    [NonSerialized] public int facingDirection = 1;
    [NonSerialized] public int wallDirection;

    [NonSerialized] public Vector2Int moveInput;

    private float _jumpInputStartTime;
    [NonSerialized] public bool jumpInput;
    [NonSerialized] public bool jumpInputHold;

    [NonSerialized] public bool grabInput;

    [NonSerialized] public bool dashInput;
    [NonSerialized] public Vector2 dashDirection;

    [NonSerialized] public bool attackInput;
    [NonSerialized] public Vector2 attackDirection;
    [NonSerialized] public Vector2 attackPoint;

    [NonSerialized] public bool abilityInput;
    [NonSerialized] public Vector2 abilityDirection;
    [NonSerialized] public Vector2 abilityPoint;

    [NonSerialized] public bool isGrounded;
    [NonSerialized] public bool isGroundClose;
    [NonSerialized] public bool isTouchingCeiling;
    [NonSerialized] public bool isTouchingCeilingWhenClimb;
    [NonSerialized] public bool isTouchingWall;
    [NonSerialized] public bool isTouchingWallBack;
    [NonSerialized] public bool isClampedBetweenWalls;
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

    private void OnDash(Vector2 point)
    {
        dashInput = true;
        dashDirection = (point - Center).normalized;
    }
    private void OnDashCanceled() => dashInput = false;

    private void OnAttack(Vector2 point)
    {
        attackInput = true;
        attackDirection = (point - Center).normalized;
        attackPoint = point;
    }
    private void OnAttackCanceled() => attackInput = false;

    private void OnAbility(Vector2 point)
    {
        abilityInput = true;
        abilityDirection = (point - Center).normalized;
        abilityPoint = point;
    }
    private void OnAbilityCanceled() => abilityInput = false;

    private void CheckJumpInputHoldTime() => jumpInput &= Time.time < _jumpInputStartTime + _jumpInputHoldTime;

    public void CheckIfGrounded()
    {
        foreach (var target in _whatIsTarget)
        {
            isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, target);
            if (isGrounded)
            {
                return;
            }
        }
    }
    public void CheckIfGroundClose()
    {
        foreach (var target in _whatIsTarget)
        {
            isGroundClose = Physics2D.Raycast(_groundChecker.position, Vector2.down, _groundCheckDistance, target);
            if (isGroundClose)
            {
                return;
            }
        }
    }

    public void CheckIfTouchingCeiling()
    {
        foreach (var target in _whatIsTarget)
        {
            isTouchingCeiling = Physics2D.OverlapCircle(_ceilingChecker.position, _groundCheckRadius, target);
            if (isTouchingCeiling)
            {
                return;
            }
        }
    }
    public void CheckIfTouchingWall()
    {
        foreach (var target in _whatIsTarget)
        {
            RaycastHit2D hit = Physics2D.Raycast(_wallChecker.position, facingDirection * Vector2.right, _wallCheckDistance, target);
            wallDirection = hit ? -facingDirection : facingDirection;
            isTouchingWall = hit;
            if (isTouchingWall)
            {
                wallPosition = new Vector2(_wallChecker.position.x + facingDirection * hit.distance, _wallChecker.position.y);
                return;
            }
        }
    }
    public void CheckIfTouchingWallBack()
    {
        foreach (var target in _whatIsTarget)
        {
            isTouchingWallBack = Physics2D.Raycast(_wallChecker.position, facingDirection * Vector2.left, _wallCheckDistance, target);
            if (isTouchingWallBack)
            {
                return;
            }
        }
    }
    public void CheckIfClampedBetweenWalls()
    {
        foreach (var target in _whatIsTarget)
        {
            isClampedBetweenWalls = Physics2D.Raycast(_wallChecker.position, Vector2.right, _wallCloseCheckDistance, target) &&
                                    Physics2D.Raycast(_wallChecker.position, Vector2.left, _wallCloseCheckDistance, target) ||
                                    Physics2D.Raycast(_ledgeChecker.position, Vector2.right, _wallCloseCheckDistance, target) &&
                                    Physics2D.Raycast(_ledgeChecker.position, Vector2.left, _wallCloseCheckDistance, target);
            if (isClampedBetweenWalls)
            {
                return;
            }
        }
    }
    public void CheckIfTouchingLedge()
    {
        foreach (var target in _whatIsTarget)
        {
            isTouchingLedge = Physics2D.Raycast(_ledgeChecker.position, facingDirection * Vector2.right, _wallCheckDistance, target);
            if (isTouchingLedge)
            {
                return;
            }
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
            isTouchingCeilingWhenClimb = Physics2D.Raycast(ledgeEndPosition, Vector2.up, _standSize.y, target);
            if (isTouchingCeilingWhenClimb)
            {
                return;
            }
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

    public void DoChecks()
    {
        CheckIfGrounded();
        CheckIfGroundClose();
        CheckIfTouchingWall();
        CheckIfTouchingWallBack();
        CheckIfTouchingCeiling();
        CheckIfTouchingCeilingWhenClimb();
        CheckIfTouchingLedge();
        CheckIfClampedBetweenWalls();
        if (isTouchingWall && !isTouchingLedge)
        {
            DeterminCornerPosition();
        }
    }

    public void TrySetVelocity(float velocity, Vector2 angle, int direction)
    {
        TrySetVelocity(new Vector2(angle.normalized.x * velocity * direction, angle.normalized.y * velocity));
    }
    public void TrySetVelocityX(float xVelocity)
    {
        TrySetVelocity(new Vector2(xVelocity, Rb.velocity.y));
    }
    public void TrySetVelocityY(float yVelocity)
    {
        TrySetVelocity(new Vector2(Rb.velocity.x, yVelocity));
    }
    public void TrySetVelocityZero()
    {
        TrySetVelocity(Vector2.zero);
    }
    public void TrySetVelocity(Vector2 velocity)
    {
        _cachedVelocity = velocity;
        if (!_isVelocityHeld)
        {
            Rb.velocity = _cachedVelocity;
        }
    }


    public void HoldVelocity(float velocity, Vector2 angle, int direction)
    {
        HoldVelocity(new Vector2(angle.normalized.x * velocity * direction, angle.normalized.y * velocity));
    }

    public void HoldVelocityX(float xVelocity)
    {
        HoldVelocity(new Vector2(xVelocity, Rb.velocity.y));
    }

    public void HoldVelocityY(float yVelocity)
    {
        HoldVelocity(new Vector2(Rb.velocity.x, yVelocity));
    }

    public void HoldVelocity(Vector2 velocity)
    {
        Rb.velocity = velocity;
        _isVelocityHeld = true;
    }

    public void ReleaseVelocity()
    {
        Rb.velocity = _cachedVelocity;
        _isVelocityHeld = false;
    }

    public void MoveToX(float x)
    {
        transform.position = new Vector2(x, transform.position.y);
    }

    public void MoveToY(float y)
    {
        transform.position = new Vector2(transform.position.x, y);
    }

    public void HoldPosition(Vector2 holdPosition)
    {
        transform.position = holdPosition;
        Rb.velocity = Vector2.zero;
    }

    public void Stand()
    {
        Collider.size = _standSize;
        Collider.offset = _standOffset;
    }

    public void Crouch()
    {
        Collider.size = _crouchSize;
        Collider.offset = _crouchOffset;
    }

    private void Awake()
    {
        Tr = GetComponent<TrailRenderer>();
        Rb = GetComponent<Rigidbody2D>();
        Collider = GetComponent<BoxCollider2D>();
        Machine = GetComponent<StateMachine>();
        Anim = GetComponent<Animator>();
        _inputReader.InitializePlayerInput(GetComponent<PlayerInput>());
        Weapon = _weaponBase.GetComponent<Weapon>();
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
        _inputReader.AttackEvent += OnAttack;
        _inputReader.AttackCanceledEvent += OnAttackCanceled;
        _inputReader.AbilityEvent += OnAbility;
        _inputReader.AbilityCanceledEvent += OnAbilityCanceled;

        foreach (var state in _states) { state.Initialize(this); }
        foreach (var ability in _abilities) { ability.Initialize(this); }
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
        _inputReader.AttackEvent -= OnAttack;
        _inputReader.AttackCanceledEvent -= OnAttackCanceled;
        _inputReader.AbilityEvent -= OnAbility;
        _inputReader.AbilityCanceledEvent -= OnAbilityCanceled;
    }

    private void Update()
    {
        Weapon.OnUpdate();
        CheckJumpInputHoldTime();
        bool abilityUsed = false;
        foreach (var ability in _abilities)
        {
            if (!abilityUsed)
            {
                abilityUsed |= ability.TryUseAbility();
            }
            ability.OnUpdate();
        }
    }

    private void OnDrawGizmos()
    {
        if (Collider != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y + Collider.offset.y), Collider.size);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(new Vector2(ledgeEndPosition.x, ledgeEndPosition.y + Collider.offset.y), Collider.size);
            Gizmos.DrawWireCube(new Vector2(ledgeStartPosition.x, ledgeStartPosition.y + Collider.offset.y), Collider.size);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(ledgeEndPosition, new Vector2(ledgeEndPosition.x, ledgeEndPosition.y + _standSize.y));
        }

        Gizmos.color = Color.white;
        Gizmos.DrawLine(new Vector2(_wallChecker.position.x - _wallCloseCheckDistance, _wallChecker.position.y), new Vector2(_wallChecker.position.x + _wallCloseCheckDistance, _wallChecker.position.y));
        Gizmos.DrawLine(new Vector2(_ledgeChecker.position.x - _wallCloseCheckDistance, _ledgeChecker.position.y), new Vector2(_ledgeChecker.position.x + _wallCloseCheckDistance, _ledgeChecker.position.y));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundChecker.position, _groundCheckRadius);
        Gizmos.DrawWireSphere(_ceilingChecker.position, _groundCheckRadius);
        Gizmos.DrawLine(_groundChecker.position, new Vector2(_groundChecker.position.x, _groundChecker.position.y - _groundCheckDistance));
        Gizmos.DrawLine(new Vector2(_wallChecker.position.x - _wallCheckDistance, _wallChecker.position.y), new Vector2(_wallChecker.position.x + _wallCheckDistance, _wallChecker.position.y));
        Gizmos.DrawLine(_ledgeChecker.position, new Vector2(_ledgeChecker.position.x + facingDirection * _wallCheckDistance, _ledgeChecker.position.y));

    }
}
