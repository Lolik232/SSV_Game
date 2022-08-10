using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerDashState DashState { get; private set; }

    [SerializeField]
    private PlayerData _playerData;
    #endregion

    #region Components

    public PlayerInputHandler InputHandler { get; private set; }

    public Animator Anim { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }

    public Transform DashDirectionIndicator { get; private set; }

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
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, _playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, _playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, _playerData, "inAir");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, _playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, _playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, _playerData, "land");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, _playerData, "wallClimb");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, _playerData, "wallGrab");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, _playerData, "wallSlide");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, _playerData, "ledgeClimb");
        DashState = new PlayerDashState(this, StateMachine, _playerData, "inAir");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Rigidbody = GetComponent<Rigidbody2D>();
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");

        StateMachine.Initialize(IdleState);

        FacingDirection = 1;
    }

    private void Update()
    {
        CurrentVelocity = Rigidbody.velocity;

        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions

    public void SetVelocityZero()
    {
        SetVelocityX(0f);
        SetVelocityY(0f);
    }

    public void SetVelocity(Single velocity, Vector2 angle, Int32 direction)
    {
        angle.Normalize();

        _workspace.Set(angle.x * velocity * direction, angle.y * velocity);

        Rigidbody.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    public void SetVelocity(Single velocity, Vector2 direction)
    {
        _workspace = direction * velocity;

        Rigidbody.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    public void SetVelocityX(Single velocity)
    {
        _workspace.Set(velocity, CurrentVelocity.y);

        Rigidbody.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    public void SetVelocityY(Single velocity)
    {
        _workspace.Set(CurrentVelocity.x, velocity);

        Rigidbody.velocity = _workspace;
        CurrentVelocity = _workspace;
    }
    #endregion

    #region Check Functions

    public Boolean CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(_groundChecker.position, _playerData.groundCheckRadius, _playerData.whatIsGround);
    }
    public Boolean CheckIfGroundIsClose()
    {
        return Physics2D.Raycast(_groundChecker.position, Vector2.down, _playerData.groundIsCloseCheckDistance, _playerData.whatIsGround);
    }

    public Boolean CheckIfTouchingWall()
    {
        return Physics2D.Raycast(_wallChecker.position, FacingDirection * Vector2.right, _playerData.wallCheckDistance, _playerData.whatIsGround);
    }

    public Boolean CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(_wallChecker.position, -FacingDirection * Vector2.right, _playerData.wallCheckDistance, _playerData.whatIsGround);
    }

    public Boolean CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(_ledgeChecker.position, FacingDirection * Vector2.right, _playerData.wallCheckDistance, _playerData.whatIsGround);
    }

    public void CheckIfShouldFlip(Int32 xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    #endregion

    #region Other Functions

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(_wallChecker.position, Vector2.right * FacingDirection, _playerData.wallCheckDistance, _playerData.whatIsGround);

        Single xDist = xHit.distance;
        _workspace.Set((xDist + 0.015f) * FacingDirection, 0f);

        RaycastHit2D yHit = Physics2D.Raycast(_ledgeChecker.position + (Vector3)_workspace, Vector2.down, _ledgeChecker.position.y - _wallChecker.position.y + 0.015f, _playerData.whatIsGround);

        Single yDist = yHit.distance;
        _workspace.Set(_wallChecker.position.x + (xDist * FacingDirection), _ledgeChecker.position.y - yDist);

        return _workspace;
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void Flip()
    {
        FacingDirection = -FacingDirection;

        var scale = transform.localScale;
        scale.x = -scale.x;

        transform.localScale = scale;

        var angle = DashDirectionIndicator.rotation;
        angle.x = 180f;

        DashDirectionIndicator.rotation = angle;
    }
    #endregion

    #region Gizmos Functions

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundChecker.position, _playerData.groundCheckRadius);

        Gizmos.DrawLine(_wallChecker.position - _playerData.wallCheckDistance * FacingDirection * Vector3.right, _wallChecker.position + _playerData.wallCheckDistance * FacingDirection * Vector3.right);
        
        Gizmos.DrawLine(_ledgeChecker.position, _ledgeChecker.position + _playerData.wallCheckDistance * FacingDirection * Vector3.right);

        Gizmos.DrawLine(_groundChecker.position, _groundChecker.position + _playerData.groundIsCloseCheckDistance * Vector3.down);
    }

    #endregion
}
