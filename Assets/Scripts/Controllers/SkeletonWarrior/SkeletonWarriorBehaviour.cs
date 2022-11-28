using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MoveController), typeof(AttackController))]

[RequireComponent(typeof(SkeletonWarriorWalkBS), typeof(SkeletonWarriorStayBS), typeof(SkeletonWarriorMoveToTargetBS))]
[RequireComponent(typeof(SkeletonWarriorAttackBS))]

public class SkeletonWarriorBehaviour : BehaviourController, IMoveController, IAttackController{
    [SerializeField] private SpriteRenderer _stan;

    
    private MoveController _moveController;
    private AttackController _attackController;

    public SkeletonWarriorWalkBS WalkCommand
    {
        get;
        private set;
    }

    public SkeletonWarriorStayBS StayCommand
    {
        get;
        private set;
    }

    public SkeletonWarriorMoveToTargetBS MoveToTargetCommand
    {
        get;
        private set;
    }

    public SkeletonWarriorAttackBS AttackCommand
    {
        get;
        private set;
    }

    public Vector2Int Move
    {
        get => ((IMoveController)_moveController).Move;
        set => ((IMoveController)_moveController).Move = value;
    }
    public Vector2 LookAt
    {
        get => ((IMoveController)_moveController).LookAt;
        set => ((IMoveController)_moveController).LookAt = value;
    }
    public bool Attack
    {
        get => ((IAttackController)_attackController).Attack;
        set => ((IAttackController)_attackController).Attack = value;
    }

    protected override void Awake()
    {
        base.Awake();
        _moveController = GetComponent<MoveController>();
        _attackController = GetComponent<AttackController>();
        WalkCommand = GetComponent<SkeletonWarriorWalkBS>();
        StayCommand = GetComponent<SkeletonWarriorStayBS>();
        MoveToTargetCommand = GetComponent<SkeletonWarriorMoveToTargetBS>();
        AttackCommand = GetComponent<SkeletonWarriorAttackBS>();

        GetBehaviourStates<SkeletonWarriorBehaviour>();
    }

    private void OnDisable()
    {
        _stan.enabled = false;
    }

    protected override void Start()
    {
        base.Start();
        _stan.enabled = false;
    }

    public override void Block()
    {
        base.Block();
        _stan.enabled = true;

        Move = Vector2Int.zero;
        GetTransition(StayCommand);
    }

    public override void Unlock()
    {
        base.Unlock();
        _stan.enabled = false;
    }
}
