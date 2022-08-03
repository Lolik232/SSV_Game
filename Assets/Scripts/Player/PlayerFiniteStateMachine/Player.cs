using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

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

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, _playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, _playerData, "move");
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
    #endregion

    #region Check Functions
    public void CheckIfShouldFlip(Int32 xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    #endregion

    #region Other Functions
    private void Flip()
    {
        FacingDirection = -FacingDirection;

        var scale = transform.localScale;
        scale.x = -scale.x;

        transform.localScale = scale;
    }
    #endregion
}
