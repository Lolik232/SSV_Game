using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }

    [SerializeField]
    private PlayerData _playerData;
    #endregion

    #region Components
    public PlayerInputHandler InputHandler { get; private set; }

    public Animator Anim { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }
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

    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, _playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, _playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, _playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, _playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, _playerData, "land");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, _playerData, "wallClimb");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, _playerData, "wallGrab");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, _playerData, "wallSlide");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Rigidbody = GetComponent<Rigidbody2D>();

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

    public Boolean CheckIftouchingWall()
    {
        return Physics2D.Raycast(_wallChecker.position, Vector2.right * FacingDirection, _playerData.wallCheckDistance, _playerData.whatIsGround);
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

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void Flip()
    {
        FacingDirection = -FacingDirection;

        var scale = transform.localScale;
        scale.x = -scale.x;

        transform.localScale = scale;
    }
    #endregion

    #region Gizmos Function

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundChecker.position, _playerData.groundCheckRadius);
       
        Gizmos.DrawLine(_wallChecker.position, _wallChecker.position + _playerData.wallCheckDistance * FacingDirection * Vector3.right);
    }

    #endregion
}
