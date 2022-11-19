using UnityEngine;

public class SkeletonWarriorWalkBS : BehaviuorState<SkeletonWarriorBehaviour>
{
    [SerializeField] private float _minTime;
    [SerializeField] private float _maxTime;

    private float _walkRandomTime;

    protected void Start()
    {
        bool StayCondition() => !Entity.TouchingEdge || Entity.TouchingWall || ActiveTime > _walkRandomTime;

        Transitions.Add(new(((SkeletonWarrior)Entity).Behaviour.StayCommand, StayCondition));
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        _walkRandomTime = Random.Range(_minTime, _maxTime);
        Controller.Move = new Vector2Int(Entity.FacingDirection, 0);
    }
}
