using UnityEngine;

public class StayAS : StayAS<MoveHorizontalAbility>
{
    protected override void Start()
    {
        base.Start();
        bool ForwardCondition() => Entity.Behaviour.Move.x == Entity.FacingDirection &&
                                                           (Mathf.Abs(Entity.Velocity.x) < 0.01f || Mathf.Sign(Entity.Behaviour.Move.x) == Mathf.Sign(Entity.Velocity.x));

        bool BackwardCondition() => Entity.Behaviour.Move.x == -Entity.FacingDirection &&
                                                                (Mathf.Abs(Entity.Velocity.x) < 0.01f || Mathf.Sign(Entity.Behaviour.Move.x) == Mathf.Sign(Entity.Velocity.x));

        Transitions.Add(new(Ability.Forward, ForwardCondition));
        Transitions.Add(new(Ability.Backward, BackwardCondition));
    }

    protected override void ApplyEnterActions()
    {
        StartSpeed = Entity.Velocity.x;
        base.ApplyEnterActions();
    }

    protected override void ApplyUpdateActions()
    {
        base.ApplyUpdateActions();
        Entity.SetVelocityX(MoveSpeed);
    }
}
