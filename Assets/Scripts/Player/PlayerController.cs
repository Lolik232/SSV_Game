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
    private float _movementSpeed = 15f;

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
    private float _jumpForce = 35f;
    [SerializeField]
    private int _maxAmountOfJumps = 1;
    [SerializeField]
    private float _movementForceInAir;
    [SerializeField]
    private float _airDragMultiplier = 0.9f;
    [SerializeField]
    private float _jumpHeightMultiplier = 0.5f;

    private int _amountOfJumpsLeft;

    [SerializeField]
    private float _wallHopForce;
    [SerializeField]
    private float _wallJumpForce;

    [SerializeField]
    private Vector2 _wallHopDirection;
    [SerializeField]
    private Vector2 _wallJumpDirection;

    private int _facingDirection = 1;


    private float _movementInputDirection;

    private bool _isFacingRight = true;
    private bool _isWalking;
    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _isWallSliding;
    private bool _canJump;

    private Rigidbody2D _rb;
    private Animator _animator;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        RestoreAmountOfJumps();

        _wallHopDirection.Normalize();
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
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckIfWallSliding()
    {
        float yMovement = _rb.velocity.y;

        _isWallSliding = _isTouchingWall && !_isGrounded && yMovement < -float.Epsilon;
    }

    private void RestoreAmountOfJumps()
    {
        _amountOfJumpsLeft = _maxAmountOfJumps;
    }

    private void CheckIfCanJump()
    {
        float yMovement = _rb.velocity.y;

        if (_isGrounded && yMovement <= 0.01f || _isWallSliding)
        {
            RestoreAmountOfJumps();
        }

        _canJump = _amountOfJumpsLeft > 0;
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
        if (_isFacingRight && _movementInputDirection < -float.Epsilon ||
            !_isFacingRight && _movementInputDirection > float.Epsilon)
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
            TryJump();
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

    private void TryJump()
    {
        if (_canJump)
        {
            if (!_isWallSliding)
            {
                NormalJump();
            }
            else if (Mathf.Abs(_movementInputDirection) > float.Epsilon)
            {
                WallJump(_wallJumpForce, _wallJumpDirection, _movementInputDirection);
            }
            else
            {
                WallJump(_wallHopForce, _wallHopDirection, -_facingDirection);
            }
        }
    }

    private void NormalJump()
    {
        float xMovement = _rb.velocity.x;
        float yMovement = _jumpForce;

        _rb.velocity = new Vector2(xMovement, yMovement);

        _amountOfJumpsLeft--;
    }

    private void WallJump(float force, Vector2 wallDirection, float unitDirection)
    {
        float xImpulse = force * wallDirection.x * unitDirection;
        float yImpulse = force * wallDirection.y;

        var forceToAdd = new Vector2(xImpulse, yImpulse);

        _rb.AddForce(forceToAdd, ForceMode2D.Impulse);

        _isWallSliding = false;

        _amountOfJumpsLeft--;
    }

    private void ApplyMovement()
    {
        float xMovement = _rb.velocity.x;
        float yMovement = _rb.velocity.y;

        if (_isGrounded)
        {
            xMovement = _movementInputDirection * _movementSpeed;
            yMovement = _rb.velocity.y;
        }
        else if (!_isWallSliding)
        {
            if (Mathf.Abs(_movementInputDirection) > float.Epsilon)
            {
                var forceToAdd = new Vector2(_movementForceInAir * _movementInputDirection, 0);

                _rb.AddForce(forceToAdd);

                if (Mathf.Abs(_rb.velocity.x) > _movementSpeed)
                {
                    xMovement = _movementSpeed * _movementInputDirection;
                }
            }
            else
            {
                xMovement *= _airDragMultiplier;
            } 
        } 

        if (_isWallSliding && yMovement < -_wallSlideSpeed)
        {
            yMovement = -_wallSlideSpeed;
        }

        _rb.velocity = new Vector2(xMovement, yMovement);
    }

    private void Flip()
    {
        if (!_isWallSliding)
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

        float xDst = position.x + (_isFacingRight ? _wallCheckDistance : -_wallCheckDistance);
        float yDst = position.y;
        float zDst = position.z;


        Gizmos.DrawLine(position, new Vector3(xDst, yDst, zDst));
    }
}
