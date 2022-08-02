using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum AnimationStates : int
    {
        Idle,
        Walk,
        Jump,
        WallSlide,
        Climb
    }

    private float _movementInputDirection;

    private int _facingDirection = 1;
    private int _lastWallJumpDirection;

    [SerializeField]
    private float _movementSpeed = 10f;
    [SerializeField]
    private float _wallSlideSpeed = 2f;
    [SerializeField]
    private float _jumpForce = 16f;
    [SerializeField]
    private float _wallJumpForce = 10f;

    [SerializeField]
    private float _airDragMultiplier = 0.9f;
    [SerializeField]
    private float _jumpHeightMultiplier = 0.5f;

    [SerializeField]
    private float _groundCheckRadius;
    [SerializeField]
    private float _wallCheckDistance;

    [SerializeField]
    private float _jumpTimerSet = 0.2f;
    [SerializeField]
    private float _turnTimerSet = 0.1f;
    [SerializeField]
    private float _wallJumpTimerSet = 0.5f;

    [SerializeField]
    private int _maxAmountOfJumps = 1;

    [SerializeField]
    private Transform _groundChecker;
    [SerializeField]
    private Transform _wallChecker;
    [SerializeField]
    private Transform _ledgeChecker;

    [SerializeField]
    private LayerMask _whatIsGround;

    [SerializeField]
    private Vector2 _ledgeClimbOffsetStart;
    [SerializeField]
    private Vector2 _ledgeClimbOffsetEnd;

    [SerializeField]
    private Vector2 _wallJumpDirection;

    private float _jumpTimer;
    private float _turnTimer;
    private float _wallJumpTimer;

    private int _amountOfJumpsLeft;

    [SerializeField]
    private bool _isFacingRight = true;
    [SerializeField]
    private bool _isWalking = false;
    [SerializeField]
    private bool _isGrounded = false;
    [SerializeField]
    private bool _isTouchingWall = false;
    [SerializeField]
    private bool _isWallSliding = false;
    [SerializeField]
    private bool _isTouchingLedge = false;
    [SerializeField]
    private bool _canNormalJump = false;
    [SerializeField]
    private bool _canWallJump = false;
    [SerializeField]
    private bool _isAttemptingToJump = false;
    [SerializeField]
    private bool _checkJumpMultiplier = false;
    [SerializeField]
    private bool _canMove = true;
    [SerializeField]
    private bool _canFlip = true;
    private bool _hasWallJumped = false;
    [SerializeField]
    private bool _canClimbLedge = false;
    [SerializeField]
    private bool _ledgeDetected = false;

    [SerializeField]
    private Vector2 _ledgePosBottom;
    [SerializeField]
    private Vector2 _ledgePosStart;
    [SerializeField]
    private Vector2 _ledgePosEnd;

    private RaycastHit2D[] _raycastBuffer = new RaycastHit2D[4];

    private Rigidbody2D _rb;
    private Animator _animator;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        RestoreAmountOfJumps();

        _wallJumpDirection.Normalize();
    }

    private void Update()
    {
        CheckInput();
        CheckMovementDirection();
        CheckWalking();
        UpdateaAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckJump();
        CheckLedgeClimb();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckIfWallSliding()
    {
        _isWallSliding = _isTouchingWall && IsInputDirectionSameTo(_facingDirection) && _rb.velocity.y < 0 && !_canClimbLedge;
    }

    private bool IsInputDirectionSameTo(float direction)
    {
        return float.IsNegative(_movementInputDirection) == float.IsNegative(direction) || !IsTryingToMove();
    }

    private bool IsTryingToMove()
    {
        return Mathf.Abs(_movementInputDirection) > 0.01f;
    }

    private void RestoreAmountOfJumps()
    {
        _amountOfJumpsLeft = _maxAmountOfJumps;
    }

    private void CheckIfCanJump()
    {
        float yMovement = _rb.velocity.y;

        if (_isGrounded && yMovement <= 0.01f)
        {
            RestoreAmountOfJumps();
        }

        _canNormalJump = _amountOfJumpsLeft > 0;

        _canWallJump = _isTouchingWall;
    }

    private void CheckLedgeClimb()
    {
        if (_ledgeDetected && !_canClimbLedge)
        {
            _canClimbLedge = true;

            float startX = _ledgePosBottom.x;
            float endX = _ledgePosBottom.x;
            float startY = _ledgePosBottom.y + _ledgeClimbOffsetStart.y;
            float endY = _ledgePosBottom.y + _ledgeClimbOffsetEnd.y;

            if (_isFacingRight)
            {
                startX += _wallCheckDistance - _ledgeClimbOffsetStart.x;
                endX += _ledgeClimbOffsetEnd.x;
            }
            else
            {
                startX += _ledgeClimbOffsetStart.x;
                endX -= _ledgeClimbOffsetEnd.x;
            }

            _ledgePosStart = new Vector2(startX, startY);
            _ledgePosEnd = new Vector2(endX, endY);

            _canMove = false;
            _canFlip = false;
        }

        if (_canClimbLedge)
        {
            _rb.position = _ledgePosStart;
        }
    }

    public void FinishLedgeClimb()
    {
        _canClimbLedge = false;
        int hitCounter = _rb.Cast(Vector2.up, _raycastBuffer, _ledgePosEnd.y - _ledgePosStart.y) + 
                         _rb.Cast(Vector2.right, _raycastBuffer, _ledgePosEnd.x - _ledgePosStart.x);

        if (hitCounter == 0)
        {
            _rb.position = _ledgePosEnd;
        }
        
        _canMove = true;
        _canFlip = true;
        _ledgeDetected = false;
    }

    private void CheckSurroundings()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, _whatIsGround);

        _isTouchingWall = Physics2D.Raycast(_wallChecker.position, Vector2.right, _wallCheckDistance * _facingDirection, _whatIsGround);
        _isTouchingLedge = Physics2D.Raycast(_ledgeChecker.position, Vector2.right, _wallCheckDistance * _facingDirection, _whatIsGround);

        if (_isTouchingWall && !_isTouchingLedge && !_ledgeDetected)
        {
            _ledgeDetected = true;
            _ledgePosBottom = _wallChecker.position;
        }
    }

    private void UpdateaAnimations()
    {
        if (_isGrounded)
        {
            if (_isWalking)
            {
                _animator.SetInteger("state", (int)AnimationStates.Walk);
            }
            else
            {
                _animator.SetInteger("state", (int)AnimationStates.Idle);
            }
        }
        else if (_canClimbLedge)
        {
            _animator.SetInteger("state", (int)AnimationStates.Climb);
        }
        else
        {
            if (_isWallSliding)
            {
                _animator.SetInteger("state", (int)AnimationStates.WallSlide);
            }
            else
            {
                _animator.SetInteger("state", (int)AnimationStates.Jump);
            }
        }
    }

    private void CheckMovementDirection()
    {
        if (!IsInputDirectionSameTo(_facingDirection))
        {
            Flip();
        }
    }

    private void CheckWalking()
    {
        float xMovement = _rb.velocity.x;

        _isWalking = _isGrounded && Mathf.Abs(xMovement) > float.Epsilon;
    }

    private void CheckInput()
    {
        _movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (_isGrounded)
            {
                Jump(NormalJump);
            }
            else
            {
                _jumpTimer = _jumpTimerSet;
                _isAttemptingToJump = true;
            }
        }

        if (Input.GetButtonDown("Horizontal") && _isTouchingWall &&
            !_isGrounded && !IsInputDirectionSameTo(_facingDirection))
        {
            _canMove = false;
            _canFlip = false;

            _turnTimer = _turnTimerSet;
        }

        if (_turnTimer > 0)
        {
            _turnTimer -= Time.deltaTime;
        }
        else
        {
            _canMove = true;
            _canFlip = true;
        }

        if (_checkJumpMultiplier && !Input.GetButton("Jump") && _rb.velocity.y > float.Epsilon)
        {
            _checkJumpMultiplier = false;
            SlowJumpDown();
        }
    }

    private void SlowJumpDown()
    {
        float xMovement = _rb.velocity.x;
        float yMovement = _rb.velocity.y * _jumpHeightMultiplier;

        _rb.velocity = new Vector2(xMovement, yMovement);
    }

    private void CheckJump()
    {
        if (_jumpTimer > 0)
        {
            if (_isGrounded)
            {
                Jump(NormalJump);
            }
            else if (_isTouchingWall &&
                    IsTryingToMove() &&
                    !IsInputDirectionSameTo(_facingDirection))
            {
                Jump(WallJump);
            }
        }

        if (_isAttemptingToJump)
        {
            _jumpTimer -= Time.deltaTime;
        }

        if (_wallJumpTimer > 0)
        {
            if (_hasWallJumped && !IsInputDirectionSameTo(_lastWallJumpDirection))
            {
                float xMovement = _rb.velocity.x;
                float yMovement = Mathf.Min(0, _rb.velocity.y);

                _rb.velocity = new Vector2(xMovement, yMovement);

                _hasWallJumped = false;
            }

            _wallJumpTimer -= Time.deltaTime;

        }
        else
        {
            _hasWallJumped = false;
        }
    }

    delegate void JumpType();

    private void Jump(JumpType jump)
    {
        jump();

        _amountOfJumpsLeft--;

        _jumpTimer = 0;
        _isAttemptingToJump = false;
        _checkJumpMultiplier = true;
    }

    private void NormalJump()
    {
        if (_canNormalJump)
        {
            float xMovement = _rb.velocity.x;
            float yMovement = _jumpForce;

            _rb.velocity = new Vector2(xMovement, yMovement);
        }
    }

    private void WallJump()
    {
        if (_canWallJump)
        {
            float xMovement = _rb.velocity.x;
            float yMovement = 0;

            _rb.velocity = new Vector2(xMovement, yMovement);

            float xImpulse = _wallJumpForce * _wallJumpDirection.x * _movementInputDirection;
            float yImpulse = _wallJumpForce * _wallJumpDirection.y;

            var forceToAdd = new Vector2(xImpulse, yImpulse);

            _rb.AddForce(forceToAdd, ForceMode2D.Impulse);

            _isWallSliding = false;

            RestoreAmountOfJumps();

            _turnTimer = 0;

            _hasWallJumped = true;
            _wallJumpTimer = _wallJumpTimerSet;
            _lastWallJumpDirection = -_facingDirection;
        }
    }

    private void ApplyMovement()
    {
        float xMovement = _rb.velocity.x;
        float yMovement = _rb.velocity.y;

        if (!_isGrounded && !_isWallSliding && !IsTryingToMove())
        {
            xMovement *= _airDragMultiplier;
        }
        else if (_canMove)
        {
            xMovement = _movementSpeed * _movementInputDirection;
        }

        if (_isWallSliding && yMovement < -_wallSlideSpeed)
        {
            yMovement = -_wallSlideSpeed;
        }

        _rb.velocity = new Vector2(xMovement, yMovement);
    }

    private void Flip()
    {
        if (!_isWallSliding && _canFlip)
        {
            _facingDirection = -_facingDirection;
            _isFacingRight = !_isFacingRight;

            var scale = transform.localScale;
            scale.x = -1 * scale.x;

            transform.localScale = scale;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundChecker.position, _groundCheckRadius);

        Vector3 position = _wallChecker.position;

        float xDst = position.x + _wallCheckDistance * _facingDirection;
        float yDst = position.y;
        float zDst = position.z;

        Gizmos.DrawLine(position, new Vector3(xDst, yDst, zDst));


        position = _ledgeChecker.position;

        xDst = position.x + _wallCheckDistance * _facingDirection;
        yDst = position.y;
        zDst = position.z;

        Gizmos.DrawLine(position, new Vector3(xDst, yDst, zDst));
    }
}
