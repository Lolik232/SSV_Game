using System;

using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStatesDescriptor StatesDescriptor { get; private set; }

    [SerializeField]
    private PlayerData _data;

    #endregion

    #region Components

    public PlayerInputHandler InputHandler { get; private set; }

    public SpriteRenderer SpriteRenderer { get; private set; }

    public Animator Animator { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }

    public Transform DashDirectionIndicator { get; private set; }

    public SpriteRenderer DashDirectionIndicatorSpriteRenderer { get; private set; }

    #endregion

    #region Other Variables

    public Vector2 CurrentVelocity { get; private set; }

    public Int32 FacingDirection { get; private set; }

    private Vector2 _workspace;

    #endregion

    #region Check Transforms

    [SerializeField]
    private Transform _groundChecker;
    [SerializeField]
    private Transform _wallChecker;
    [SerializeField]
    private Transform _ledgeChecker;

    #endregion

    #region Unity Callback Functions

    private void Awake()
    {
        StatesDescriptor = new PlayerStatesDescriptor(this, _data);
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Rigidbody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        DashDirectionIndicator = transform.Find("DashDirectionIndicator");
        DashDirectionIndicatorSpriteRenderer = DashDirectionIndicator.gameObject.GetComponent<SpriteRenderer>();

        InputHandler.Initialize(_data);
        StatesDescriptor.Initialize();

        FacingDirection = 1;
    }

    private void Update()
    {
        CurrentVelocity = Rigidbody.velocity;
        StatesDescriptor.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StatesDescriptor.PhysicsUpdate();
    }

    #endregion

    #region Set Functions

    public void ResetVelocity()
    {
        SetVelocity(0f, Vector2.zero);
    }

    public void SetVelocity(Single velocity, Vector2 angle, Int32 direction)
    {
        angle.Normalize();
        _workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        Rigidbody.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    public void SetVelocity(Single velocity, Vector2 angle)
    {
        _workspace = velocity * angle;
        Rigidbody.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    public void ResetVelocityX()
    {
        SetVelocityX(0f);
    }

    public void SetVelocityX(Single velocity)
    {
        _workspace.Set(velocity, CurrentVelocity.y);
        Rigidbody.velocity = _workspace;
        CurrentVelocity = _workspace;
    }
    public void ResetVelocityY()
    {
        SetVelocityY(0f);
    }

    public void SetVelocityY(Single velocity)
    {
        _workspace.Set(CurrentVelocity.x, velocity);
        Rigidbody.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    #endregion

    #region Other Functions

    private void AnimationTrigger() => StatesDescriptor.AnimationTrigger();

    private void AnimationFinishTrigger() => StatesDescriptor.AnimationFinishTrigger();

    private void Flip()
    {
        FacingDirection = -FacingDirection;

        Flip(SpriteRenderer);
        Flip(DashDirectionIndicatorSpriteRenderer);
    }

    private void Flip(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    #endregion

    #region Check Functions

    public Boolean CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(_groundChecker.position,
            _data.groundCheckRadius,
            _data.whatIsGround);
    }
    public Boolean CheckIfGroundFar()
    {
        return !Physics2D.Raycast(_groundChecker.position,
            Vector2.down,
            _data.groundIsCloseCheckDistance,
            _data.whatIsGround);
    }

    public Boolean CheckIfTouchingWall()
    {
        return Physics2D.Raycast(_wallChecker.position,
            FacingDirection * Vector2.right,
            _data.wallCheckDistance,
            _data.whatIsGround);
    }

    public Boolean CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(_wallChecker.position,
            -FacingDirection * Vector2.right,
            _data.wallCheckDistance,
            _data.whatIsGround);
    }

    public Boolean CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(_ledgeChecker.position,
            FacingDirection * Vector2.right,
            _data.wallCheckDistance,
            _data.whatIsGround);
    }

    public void CheckIfShouldFlip(Int32 xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    #endregion

    #region Gizmos Functions

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundChecker.position, _data.groundCheckRadius);
        Gizmos.DrawLine(_wallChecker.position - _data.wallCheckDistance * FacingDirection * Vector3.right, _wallChecker.position + _data.wallCheckDistance * FacingDirection * Vector3.right);
        Gizmos.DrawLine(_ledgeChecker.position, _ledgeChecker.position + _data.wallCheckDistance * FacingDirection * Vector3.right);
        Gizmos.DrawLine(_groundChecker.position, _groundChecker.position + _data.groundIsCloseCheckDistance * Vector3.down);
    }

    #endregion
}
