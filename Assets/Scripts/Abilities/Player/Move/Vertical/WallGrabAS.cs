﻿public class WallGrabAS : StayAS<MoveOnWallAbility>
{
    protected override void Start()
    {
        base.Start();
        bool ClimbCondition() => Entity.Behaviour.Grab && Entity.Behaviour.Move.y == 1 &&
                                                         Entity.Velocity.y > -0.01f;

        bool SlideCondition() => (Entity.Behaviour.Grab && Entity.Behaviour.Move.y == -1 || !Entity.Behaviour.Grab) &&
                                                         Entity.Velocity.y < 0.01f || Entity.Behaviour.Attack;

        Transitions.Add(new(Ability.Climb, ClimbCondition));
        Transitions.Add(new(Ability.Slide, SlideCondition));
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