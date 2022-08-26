using System;

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInputHandler), typeof(Animator))]
public class Player : MonoBehaviour
{
    public EnvironmentCheckersManager EnvironmentCheckersManager { get; private set; }

    public PlayerStatesManager StatesManager { get; private set; }

    public PlayerAnimationController AnimationController { get; private set; }

    public PlayerMoveController MoveController { get; private set; }

    public PlayerInputHandler InputHandler { get; private set; }

    public Rigidbody2D RB { get; private set; }

    public Animator Anim { get; private set; }

    [SerializeField] private Transform m_GroundChecker;
    [SerializeField] private Transform m_WallChecker;
    [SerializeField] private Transform m_LedgeChecker;


    [SerializeField] private PlayerData m_Data;
    public PlayerData Data { get => m_Data; private set => m_Data = value; }

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Anim = GetComponent<Animator>();

        MoveController = new PlayerMoveController(transform, RB, InputHandler);
        EnvironmentCheckersManager = new EnvironmentCheckersManager(m_GroundChecker, m_WallChecker, m_LedgeChecker, MoveController, Data);
        StatesManager = new PlayerStatesManager(EnvironmentCheckersManager, MoveController, InputHandler, Data);
        AnimationController = new PlayerAnimationController(Anim, StatesManager.StateMachine);
    }

    private void Start()
    {
        EnvironmentCheckersManager.Initialize();
        StatesManager.Initialize();
        MoveController.Initialize();
        AnimationController.Initialize();
    }

    private void Update()
    {
        StatesManager.LogicUpdate();
        MoveController.LogicUpdate();
    }

    private void FixedUpdate()
    {
        EnvironmentCheckersManager.PhysicsUpdate();
    }

    private void OnDrawGizmos()
    {
        EnvironmentCheckersManager?.OnDrawGizmos();
    }
}
