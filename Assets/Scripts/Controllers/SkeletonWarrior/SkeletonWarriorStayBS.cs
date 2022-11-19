using UnityEngine;

public class SkeletonWarriorStayBS : BehaviuorState<SkeletonWarriorBehaviour>
{
    [SerializeField] private float _minTime;
    [SerializeField] private float _maxTime;

    private float _stayRandomTime;

    protected void Start()
    {
        bool WalkCondition() => ActiveTime > _stayRandomTime;

        Transitions.Add(new(((SkeletonWarrior)Entity).Behaviour.WalkCommand, WalkCondition));
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        _stayRandomTime = Random.Range(_minTime, _maxTime);
        Controller.Move = new Vector2Int(0, 0);
    }
    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        Entity.RotateIntoDirection(-Entity.FacingDirection);
    }
}
