using UnityEngine;

public class SkeletonWarriorWalkBS : BehaviuorState<SkeletonWarriorBehaviour>
{
    protected void Start()
    {
        bool StayCondition() => !Entity.TouchingEdge || Entity.TouchingWall;

        bool MoveToTargetCondition() => Entity.TargetDetected;

        Transitions.Add(new(Controller.StayCommand, StayCondition));
        Transitions.Add(new(Controller.MoveToTargetCommand, MoveToTargetCondition));
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        Controller.Move = new Vector2Int(Entity.FacingDirection, 0);
    }
}
