//using UnityEngine;
//using System;

//public class PlayerController : MonoBehaviour
//{
//    private enum AnimationStates : Int32
//    {
//        Idle,
//        Walk,
//        Jump,
//        WallSlide,
//        LedgeClimb
//    }

//    private enum Direction : Byte
//    {
//        Left, 
//        Right
//    }

//    private Single _movementInputDirection;

//    private Direction _facingDirection = Direction.Right;
//    private Direction _lastWallJumpDirection;

//    [SerializeField]
//    private Single _maxControlledMoveSpeed;
//    [SerializeField]
//    private Single _wallSlideSpeed;
//    [SerializeField]
//    private Single _normalJumpForce;
//    [SerializeField]
//    private Single _wallJumpForce;

//    [SerializeField]
//    private Single _airDragMultiplier;
//    [SerializeField]
//    private Single _jumpHeightDragMultiplier;

//    [SerializeField]
//    private Single _groundCheckRadius;
//    [SerializeField]
//    private Single _wallCheckDistance;

//    [SerializeField]
//    private Single _normalJumpWaitingTimerSet;
//    [SerializeField]
//    private Single _wallJumpWaitingTimerSet;

//    [SerializeField]
//    private Int32 _maxAmountOfNormalJumps;

//    [SerializeField]
//    private Transform _groundChecker;
//    [SerializeField]
//    private Transform _wallChecker;
//    [SerializeField]
//    private Transform _ledgeChecker;

//    [SerializeField]
//    private LayerMask _whatIsGround;

//    [SerializeField]
//    private Vector2 _ledgeClimbOffsetStart;
//    [SerializeField]
//    private Vector2 _ledgeClimbOffsetEnd;

//    [SerializeField]
//    private Vector2 _wallJumpDirection;

//    private Single _normalJumpWaitingTimer;
//    private Single _wallJumpWaitingTimer;

//    private Int32 _amountOfNormalJumpsLeft;

//    private Boolean _isGrounded;
//    private Boolean _isWalking;
//    private Boolean _isTouchingWall;
//    private Boolean _isledgeDetected;
//    private Boolean _isTouchingLedge;
//    private Boolean _isWallSliding;
//    private Boolean _isClimbingLedge;
//    private Boolean _canNormalJump;
//    private Boolean _canWallJump;
//    private Boolean _isWaitingForJump;
//    private Boolean _needToCheckJumpMultiplier;
//    private Boolean _hasWallJumped;
//    private Boolean _canMakeControlledMovement = true;
//    private Boolean _canFlip = true;

//    private Vector2 _ledgePositionBottom;
//    private Vector2 _ledgePositionStart;
//    private Vector2 _ledgePositionEnd;

//    private Rigidbody2D _rb;
//    private Animator _anim;

//    private AnimationStates _animState;

//    private void Start()
//    {
//        _rb = GetComponent<Rigidbody2D>();
//        _anim = GetComponent<Animator>();

//        RestoreAmountOfJumps();

//        _wallJumpDirection.Normalize();
//    }

//    private void Update()
//    {
//        CheckInput();
//        CheckMovementDirection();
//        CheckWalking();
//        UpdateaAnimations();
//        CheckIfCanJump();
//        CheckIfWallSliding();
//        CheckJump();
//        CheckLedgeClimb();
//    }

//    private void FixedUpdate()
//    {
//        ApplyMovement();
//        CheckSurroundings();
//    }

//    private void CheckIfWallSliding()
//    {
//        _isWallSliding = _isTouchingWall && IsInputDirectionSameTo(_facingDirection) && _rb.velocity.y < 0 && !_isClimbingLedge;
//    }

//    private bool IsInputDirectionSameTo(float direction)
//    {
//        return float.IsNegative(_movementInputDirection) == float.IsNegative(direction) || !IsTryingToMove();
//    }

//    private bool IsTryingToMove()
//    {
//        return Mathf.Abs(_movementInputDirection) > 0.01f;
//    }

//    private void RestoreAmountOfJumps()
//    {
//        _amountOfNormalJumpsLeft = _maxAmountOfNormalJumps;
//    }

//    private void CheckIfCanJump()
//    {
//        float yMovement = _rb.velocity.y;

//        if (_isGrounded && yMovement <= 0.01f)
//        {
//            RestoreAmountOfJumps();
//        }

//        _canNormalJump = _amountOfNormalJumpsLeft > 0;

//        _canWallJump = _isTouchingWall;
//    }

//    private void CheckLedgeClimb()
//    {
//        if (_isledgeDetected && !_isClimbingLedge)
//        {
//            _isClimbingLedge = true;

//            float startX = _ledgePositionBottom.x;
//            float endX = _ledgePositionBottom.x;
//            float startY = _ledgePositionBottom.y + _ledgeClimbOffsetStart.y;
//            float endY = _ledgePositionBottom.y + _ledgeClimbOffsetEnd.y;

//            if (_isFacingRight)
//            {
//                startX += _wallCheckDistance - _ledgeClimbOffsetStart.x;
//                endX += _ledgeClimbOffsetEnd.x;
//            }
//            else
//            {
//                startX += _ledgeClimbOffsetStart.x;
//                endX -= _ledgeClimbOffsetEnd.x;
//            }

//            _ledgePositionStart = new Vector2(startX, startY);
//            _ledgePositionEnd = new Vector2(endX, endY);

//            _canMakeControlledMovement = false;
//            _canFlip = false;
//        }

//        if (_isClimbingLedge)
//        {
//            _rb.position = _ledgePositionStart;
//        }
//    }

//    public void FinishLedgeClimb()
//    {
//        _isClimbingLedge = false;
//        int hitCounter = _rb.Cast(Vector2.up, _raycastBuffer, _ledgePositionEnd.y - _ledgePositionStart.y) + 
//                         _rb.Cast(Vector2.right, _raycastBuffer, _ledgePositionEnd.x - _ledgePositionStart.x);

//        if (hitCounter == 0)
//        {
//            _rb.position = _ledgePositionEnd;
//        }
        
//        _canMakeControlledMovement = true;
//        _canFlip = true;
//        _isledgeDetected = false;
//    }

//    private void CheckSurroundings()
//    {
//        _isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, _whatIsGround);

//        _isTouchingWall = Physics2D.Raycast(_wallChecker.position, Vector2.right, _wallCheckDistance * _facingDirection, _whatIsGround);
//        _isTouchingLedge = Physics2D.Raycast(_ledgeChecker.position, Vector2.right, _wallCheckDistance * _facingDirection, _whatIsGround);

//        if (_isTouchingWall && !_isTouchingLedge && !_isledgeDetected)
//        {
//            _isledgeDetected = true;
//            _ledgePositionBottom = _wallChecker.position;
//        }
//    }

//    private void UpdateaAnimations()
//    {
//        if (_isGrounded)
//        {
//            if (_isWalking)
//            {
//                _anim.SetInteger("state", (int)AnimationStates.Walk);
//            }
//            else
//            {
//                _anim.SetInteger("state", (int)AnimationStates.Idle);
//            }
//        }
//        else if (_isClimbingLedge)
//        {
//            _anim.SetInteger("state", (int)AnimationStates.LedgeClimb);
//        }
//        else
//        {
//            if (_isWallSliding)
//            {
//                _anim.SetInteger("state", (int)AnimationStates.WallSlide);
//            }
//            else
//            {
//                _anim.SetInteger("state", (int)AnimationStates.Jump);
//            }
//        }
//    }

//    private void CheckMovementDirection()
//    {
//        if (!IsInputDirectionSameTo(_facingDirection))
//        {
//            Flip();
//        }
//    }

//    private void CheckWalking()
//    {
//        float xMovement = _rb.velocity.x;

//        _isWalking = _isGrounded && Mathf.Abs(xMovement) > float.Epsilon;
//    }

//    private void CheckInput()
//    {
//        _movementInputDirection = Input.GetAxisRaw("Horizontal");

//        if (Input.GetButtonDown("Jump"))
//        {
//            if (_isGrounded)
//            {
//                Jump(NormalJump);
//            }
//            else
//            {
//                _normalJumpWaitingTimer = _normalJumpWaitingTimerSet;
//                _isWaitingForJump = true;
//            }
//        }

//        if (Input.GetButtonDown("Horizontal") && _isTouchingWall &&
//            !_isGrounded && !IsInputDirectionSameTo(_facingDirection))
//        {
//            _canMakeControlledMovement = false;
//            _canFlip = false;

//            _wallJumpWaitingTimer = _wallJumpWaitingTimerSet;
//        }

//        if (_wallJumpWaitingTimer > 0)
//        {
//            _wallJumpWaitingTimer -= Time.deltaTime;
//        }
//        else
//        {
//            _canMakeControlledMovement = true;
//            _canFlip = true;
//        }

//        if (_needToCheckJumpMultiplier && !Input.GetButton("Jump") && _rb.velocity.y > float.Epsilon)
//        {
//            _needToCheckJumpMultiplier = false;
//            SlowJumpDown();
//        }
//    }

//    private void SlowJumpDown()
//    {
//        float xMovement = _rb.velocity.x;
//        float yMovement = _rb.velocity.y * _jumpHeightDragMultiplier;

//        _rb.velocity = new Vector2(xMovement, yMovement);
//    }

//    private void CheckJump()
//    {
//        if (_normalJumpWaitingTimer > 0)
//        {
//            if (_isGrounded)
//            {
//                Jump(NormalJump);
//            }
//            else if (_isTouchingWall &&
//                    IsTryingToMove() &&
//                    !IsInputDirectionSameTo(_facingDirection))
//            {
//                Jump(WallJump);
//            }
//        }

//        if (_isWaitingForJump)
//        {
//            _normalJumpWaitingTimer -= Time.deltaTime;
//        }

//        if (_wallJumpTimer > 0)
//        {
//            if (_hasWallJumped && !IsInputDirectionSameTo(_lastWallJumpDirection))
//            {
//                float xMovement = _rb.velocity.x;
//                float yMovement = Mathf.Min(0, _rb.velocity.y);

//                _rb.velocity = new Vector2(xMovement, yMovement);

//                _hasWallJumped = false;
//            }

//            _wallJumpTimer -= Time.deltaTime;

//        }
//        else
//        {
//            _hasWallJumped = false;
//        }
//    }

//    delegate void JumpType();

//    private void Jump(JumpType jump)
//    {
//        jump();

//        _amountOfNormalJumpsLeft--;

//        _normalJumpWaitingTimer = 0;
//        _isWaitingForJump = false;
//        _needToCheckJumpMultiplier = true;
//    }

//    private void NormalJump()
//    {
//        if (_canNormalJump)
//        {
//            float xMovement = _rb.velocity.x;
//            float yMovement = _normalJumpForce;

//            _rb.velocity = new Vector2(xMovement, yMovement);
//        }
//    }

//    private void WallJump()
//    {
//        if (_canWallJump)
//        {
//            float xMovement = _rb.velocity.x;
//            float yMovement = 0;

//            _rb.velocity = new Vector2(xMovement, yMovement);

//            float xImpulse = _wallJumpForce * _wallJumpDirection.x * _movementInputDirection;
//            float yImpulse = _wallJumpForce * _wallJumpDirection.y;

//            var forceToAdd = new Vector2(xImpulse, yImpulse);

//            _rb.AddForce(forceToAdd, ForceMode2D.Impulse);

//            _isWallSliding = false;

//            RestoreAmountOfJumps();

//            _wallJumpWaitingTimer = 0;

//            _hasWallJumped = true;
//            _wallJumpTimer = _wallJumpTurn;
//            _lastWallJumpDirection = -_facingDirection;
//        }
//    }

//    private void ApplyMovement()
//    {
//        float xMovement = _rb.velocity.x;
//        float yMovement = _rb.velocity.y;

//        if (!_isGrounded && !_isWallSliding && !IsTryingToMove())
//        {
//            xMovement *= _airDragMultiplier;
//        }
//        else if (_canMakeControlledMovement)
//        {
//            xMovement = _maxControlledMoveSpeed * _movementInputDirection;
//        }

//        if (_isWallSliding && yMovement < -_wallSlideSpeed)
//        {
//            yMovement = -_wallSlideSpeed;
//        }

//        _rb.velocity = new Vector2(xMovement, yMovement);
//    }

//    private void Flip()
//    {
//        if (!_isWallSliding && _canFlip)
//        {
//            _facingDirection = -_facingDirection;
//            _isFacingRight = !_isFacingRight;

//            var scale = transform.localScale;
//            scale.x = -1 * scale.x;

//            transform.localScale = scale;
//        }
//    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.DrawWireSphere(_groundChecker.position, _groundCheckRadius);

//        Vector3 position = _wallChecker.position;

//        float xDst = position.x + _wallCheckDistance * _facingDirection;
//        float yDst = position.y;
//        float zDst = position.z;

//        Gizmos.DrawLine(position, new Vector3(xDst, yDst, zDst));


//        position = _ledgeChecker.position;

//        xDst = position.x + _wallCheckDistance * _facingDirection;
//        yDst = position.y;
//        zDst = position.z;

//        Gizmos.DrawLine(position, new Vector3(xDst, yDst, zDst));
//    }
//}
