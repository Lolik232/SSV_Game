using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum AnimationStates : int
    {
        Idle,
        Walk,
        Jump,
        WallSlide
    }

    [SerializeField]
    private float _movementSpeed = 10f;

    [SerializeField]
    private LayerMask _whatIsGround;

    [SerializeField]
    private Transform _groundChecker;
    [SerializeField]
    private float _groundCheckRadius;

    [SerializeField]
    private Transform _wallChecker;
    [SerializeField]
    private float _wallCheckDistance;
    [SerializeField]
    private float _wallSlideSpeed;

    [SerializeField]
    private float _jumpForce = 16f;
    [SerializeField]
    private int _maxAmountOfJumps = 1;
    [SerializeField]
    private float _airDragMultiplier = 0.9f;
    [SerializeField]
    private float _jumpHeightMultiplier = 0.5f;

    private float _jumpTimer;

    [SerializeField]
    private float _jumpTimerSet = 0.2f;

    private int _amountOfJumpsLeft;

    [SerializeField]
    private float _wallJumpForce;
    [SerializeField]
    private Vector2 _wallJumpDirection;

    private float _movementInputDirection;

    private bool _isFacingRight = true;
    private bool _isWalking;
    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _isWallSliding;
    private bool _canNormalJump;
    private bool _canWallJump;
    private bool _isAttemptingToJump;

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
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckIfWallSliding()
    {
        _isWallSliding = _isTouchingWall && IsInputDirectionSameToFacingDirection();
    }

    private bool IsInputDirectionSameToFacingDirection()
    {
        return float.IsNegative(_movementInputDirection) == !_isFacingRight || !IsTryingToMove();
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

    private void CheckSurroundings()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, _whatIsGround);

        Vector2 wallCheckDirection = _isFacingRight ? Vector2.right : Vector2.left;
        _isTouchingWall = Physics2D.Raycast(_wallChecker.position, wallCheckDirection, _wallCheckDistance, _whatIsGround);
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
        if (!IsInputDirectionSameToFacingDirection())
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

        if (Input.GetButtonUp("Jump") && _rb.velocity.y > float.Epsilon)
        {
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
                    !IsInputDirectionSameToFacingDirection())
            {
                Jump(WallJump);
            }
        }

        if (_isAttemptingToJump)
        {
            _jumpTimer -= Time.deltaTime;
        }
    }

    delegate void JumpType();

    private void Jump(JumpType jump)
    {
        jump();

        _amountOfJumpsLeft--;

        _jumpTimer = 0;
        _isAttemptingToJump = false;
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
        else if (_isWallSliding && yMovement < -_wallSlideSpeed)
        {
            yMovement = -_wallSlideSpeed;
        }
        else
        {
            xMovement = _movementSpeed * _movementInputDirection;
        }

        _rb.velocity = new Vector2(xMovement, yMovement);
    }

    private void Flip()
    {
        if (!_isWallSliding)
        {
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

        float xDst = position.x + (_isFacingRight ? _wallCheckDistance : -_wallCheckDistance);
        float yDst = position.y;
        float zDst = position.z;


        Gizmos.DrawLine(position, new Vector3(xDst, yDst, zDst));
    }
}
