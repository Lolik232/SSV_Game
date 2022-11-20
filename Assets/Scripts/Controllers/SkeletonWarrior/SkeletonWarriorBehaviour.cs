using UnityEngine;

[RequireComponent(typeof(MoveController), typeof(AttackController))]

[RequireComponent(typeof(SkeletonWarriorWalkBS), typeof(SkeletonWarriorStayBS))]

public class SkeletonWarriorBehaviour : BehaviourController, IMoveController, IAttackController
{
    [SerializeField] private Entity _target;

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

        GetBehaviourStates<SkeletonWarriorBehaviour>();
    }

    protected override void Update()
    {
        base.Update();
        _moveController.LookAt = _target.transform.position;
    }
}
