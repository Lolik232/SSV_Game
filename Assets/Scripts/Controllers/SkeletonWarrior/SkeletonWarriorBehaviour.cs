using UnityEngine;

[RequireComponent(typeof(MoveController), typeof(AttackController))]

public class SkeletonWarriorBehaviour : Component, IMoveController, IAttackController
{
    [SerializeField] private Entity _target;

    private SkeletonWarrior _skeleton;

    private MoveController _moveController;
    private AttackController _attackController;

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

    private void Awake()
    {
        _skeleton = GetComponent<SkeletonWarrior>();
        _moveController = GetComponent<MoveController>();
        _attackController = GetComponent<AttackController>();
    }

    private void Update()
    {
        _moveController.LookAt = _target.transform.position;
    }
}
