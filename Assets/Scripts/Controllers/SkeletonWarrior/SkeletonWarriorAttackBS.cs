using System.Collections;

using UnityEngine;

public class SkeletonWarriorAttackBS : BehaviuorState<SkeletonWarriorBehaviour>
{
    [SerializeField] private float _prepareForAttackTime;    

    private bool _prepareForAttack;

    protected void Start()
    {
        bool MoveToTargetCondition() => !_prepareForAttack;

        Transitions.Add(new(Controller.MoveToTargetCommand, MoveToTargetCondition));
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        Controller.Move = Vector2Int.zero;
        _prepareForAttack = true;
        Controller.LookAt = Entity.TargetPosition;
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        while (ActiveTime < _prepareForAttackTime && Entity.AttackPermited)
        {
            yield return null;
            Controller.LookAt = Entity.TargetPosition;
        }

        _prepareForAttack = false;
        if (Entity.AttackPermited && !Entity.Behaviour.IsLocked)
        {
            Controller.Attack = true;
        }
    }
}
