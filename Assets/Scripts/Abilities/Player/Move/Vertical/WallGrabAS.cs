using UnityEngine;

public class WallGrabAS : StayAS<MoveOnWallAbility>
{
    protected override void Start()
    {
        base.Start();
        bool ClimbCondition() => Entity.Behaviour.Grab && Entity.Behaviour.Move.y == 1 &&
                                                         Mathf.Abs(Entity.Velocity.y) < 0.01f;

        bool SlideCondition() => (Entity.Behaviour.Grab && Entity.Behaviour.Move.y == -1 || !Entity.Behaviour.Grab) &&
                                                         Mathf.Abs(Entity.Velocity.y) < 0.01f || !Entity.AttackAbility.CanWallClimb;

        Transitions.Add(new(Ability.Slide, SlideCondition));
        Transitions.Add(new(Ability.Climb, ClimbCondition));
    }

    protected override void ApplyEnterActions()
    {
        StartSpeed = Entity.Velocity.y;
        base.ApplyEnterActions();
    }

    protected override void ApplyUpdateActions()
    {
        base.ApplyUpdateActions();
        Entity.SetVelocityY(MoveSpeed);
    }
}
