using UnityEngine;

public class SkeletonWarriorStayBS : BehaviuorState<SkeletonWarriorBehaviour>
{
    [SerializeField] private float _minTime;
    [SerializeField] private float _maxTime;

    private float _stayRandomTime;

    protected void Start()
    {
        bool WalkCondition() => ActiveTime > _stayRandomTime && !Entity.TargetDetected;

        bool MoveToTargetCondition() => Entity.TargetDetected && !(Entity.TargetDirection == Entity.FacingDirection &&
                                                                   !Entity.TouchingEdge || Entity.TouchingWall);

        Transitions.Add(new(Controller.WalkCommand, WalkCondition));
        Transitions.Add(new(Controller.MoveToTargetCommand, MoveToTargetCondition));
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        _stayRandomTime = Random.Range(_minTime, _maxTime);
        Controller.Move = Vector2Int.zero;
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        Entity.RotateIntoDirection(-Entity.FacingDirection);
    }
}
