using System;

using UnityEngine;

public class Player : MonoBehaviour
{
    #region Characteristics

    public PlayerCharacteristic Endurance { get; private set; }

    #endregion

    #region State Variables
    public PlayerStatesDescriptor StatesDescriptor { get; private set; }

    [SerializeField]
    private PlayerData m_Data;

    #endregion

    #region Components

    public PlayerInputHandler InputHandler { get; private set; }

    public Animator Animator { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }

    public Transform DashDirectionIndicator { get; private set; }

    #endregion

    #region Other Variables

    public Vector2 CurrentVelocity { get; private set; }

    public Int32 FacingDirection { get; private set; }

    private Vector2 m_Workspace;

    #endregion

    #region Check Transforms

    [SerializeField]
    private Transform m_GroundChecker;
    [SerializeField]
    private Transform m_WallChecker;
    [SerializeField]
    private Transform m_LedgeChecker;

    #endregion

    #region Unity Callback Functions

    private void Awake()
    {
        Endurance = new PlayerCharacteristic(6f, 4f);
        StatesDescriptor = new PlayerStatesDescriptor(this, m_Data);
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Rigidbody = GetComponent<Rigidbody2D>();

        DashDirectionIndicator = transform.Find("DashDirectionIndicator");

        StatesDescriptor.InitializeStateMachine();

        FacingDirection = 1;
    }

    private void Update()
    {
        CurrentVelocity = Rigidbody.velocity;
        StatesDescriptor.LogicUpdate();

        Endurance.RestorePoints();
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
        m_Workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        Rigidbody.velocity = m_Workspace;
        CurrentVelocity = m_Workspace;
    }

    public void SetVelocity(Single velocity, Vector2 angle)
    {
        m_Workspace = velocity * angle;
        Rigidbody.velocity = m_Workspace;
        CurrentVelocity = m_Workspace;
    }

    public void ResetVelocityX()
    {
        SetVelocityX(0f);
    }

    public void SetVelocityX(Single velocity)
    {
        m_Workspace.Set(velocity, CurrentVelocity.y);
        Rigidbody.velocity = m_Workspace;
        CurrentVelocity = m_Workspace;
    }
    public void ResetVelocityY()
    {
        SetVelocityY(0f);
    }

    public void SetVelocityY(Single velocity)
    {
        m_Workspace.Set(CurrentVelocity.x, velocity);
        Rigidbody.velocity = m_Workspace;
        CurrentVelocity = m_Workspace;
    }

    #endregion

    #region Other Functions

    private void AnimationTrigger(Int32 id = 0) => StatesDescriptor.AnimationTrigger(id);

    private void AnimationFinishTrigger() => StatesDescriptor.AnimationFinishTrigger();

    private void Flip()
    {
        FacingDirection = -FacingDirection;

        Flip(transform);
        Flip(DashDirectionIndicator);
    }

    private void Flip(Transform targetTransform)
    {
        targetTransform.Rotate(0f, 180f, 0f);
    }

    #endregion

    #region Check Functions

    public Boolean CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(m_GroundChecker.position,
            m_Data.groundCheckRadius,
            m_Data.whatIsGround);
    }
    public Boolean CheckIfGroundFar()
    {
        return !Physics2D.Raycast(m_GroundChecker.position,
            Vector2.down,
            m_Data.groundIsCloseCheckDistance,
            m_Data.whatIsGround);
    }

    public Boolean CheckIfTouchingWall()
    {
        return Physics2D.Raycast(m_WallChecker.position,
            FacingDirection * Vector2.right,
            m_Data.wallCheckDistance,
            m_Data.whatIsGround);
    }

    public Boolean CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(m_WallChecker.position,
            -FacingDirection * Vector2.right,
            m_Data.wallCheckDistance,
            m_Data.whatIsGround);
    }

    public Boolean CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(m_LedgeChecker.position,
            FacingDirection * Vector2.right,
            m_Data.wallCheckDistance,
            m_Data.whatIsGround);
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
        Gizmos.DrawWireSphere(m_GroundChecker.position, m_Data.groundCheckRadius);
        Gizmos.DrawLine(m_WallChecker.position - m_Data.wallCheckDistance * FacingDirection * Vector3.right, m_WallChecker.position + m_Data.wallCheckDistance * FacingDirection * Vector3.right);
        Gizmos.DrawLine(m_LedgeChecker.position, m_LedgeChecker.position + m_Data.wallCheckDistance * FacingDirection * Vector3.right);
        Gizmos.DrawLine(m_GroundChecker.position, m_GroundChecker.position + m_Data.groundIsCloseCheckDistance * Vector3.down);
    }

    #endregion
}
