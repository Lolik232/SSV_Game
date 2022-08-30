using System;

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInputHandler), typeof(Animator))]
public class Player : Unit
{
    public PlayerCharacteristicsManager CharacteristicsManager { get; private set; }

    public PlayerStatesManager StatesManager { get; private set; }

    public PlayerAnimationController AnimationController { get; private set; }

    public new PlayerMoveController MoveController { get => (PlayerMoveController)base.MoveController; private set => base.MoveController = value; }

    public PlayerInputHandler InputHandler { get; private set; }

    public PlayerAbilitiesManager AbilitiesManager { get; private set; }

    public Animator Anim { get; private set; }

    private new PlayerData Data { get => (PlayerData)base.Data; set => base.Data = value; }

    protected override void Awake()
    {
        base.Awake();
        InputHandler = GetComponent<PlayerInputHandler>();
        Anim = GetComponent<Animator>();

        MoveController = new PlayerMoveController(this, Data);
        StatesManager = new PlayerStatesManager(this, Data);
        AnimationController = new PlayerAnimationController(this);
        AbilitiesManager = new PlayerAbilitiesManager(this, Data);
        CharacteristicsManager = new PlayerCharacteristicsManager(this, Data);
    }

    protected override void Start()
    {
        base.Start();
        AnimationController.Initialize();
        AbilitiesManager.Initialize();
        CharacteristicsManager.Initialize();
    }

    protected override void Update()
    {
        base.Update();
        StatesManager.StateMachine.CurrentState.LogicUpdate();
        CharacteristicsManager.LogicUpdate();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        StatesManager.StateMachine.CurrentState.PhysicsUpdate();
        CharacteristicsManager.PhysicsUpdate();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        EnvironmentCheckersManager?.OnDrawGizmos();
    }

    private void OnLandFinished()
    {
        StatesManager.LandState.OnLandFinished();
    }
}
