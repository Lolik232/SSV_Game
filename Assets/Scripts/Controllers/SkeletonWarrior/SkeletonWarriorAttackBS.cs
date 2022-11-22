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
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        _prepareForAttack = true;
        while (ActiveTime < _prepareForAttackTime)
        {
            yield return null;
        }

        _prepareForAttack = false;
        if (Entity.TargetDetected && Entity.TargetDistance < ((MeleeWeapon)Entity.Inventory.Current).Length)
        {
            Controller.LookAt = Entity.TargetPosition;
            Controller.Attack = true;
        }
    }
}
