﻿public class WallClimbAS : MoveAS<MoveOnWallAbility>
{
    protected override void Start()
    {
        base.Start();
        bool GrabCondition() => !Entity.Behaviour.Grab || Entity.Behaviour.Move.y != 1;
        bool SlideCondition() => Entity.AttackAbility.IsActive;

        Transitions.Add(new(Ability.Grab, GrabCondition));
        Transitions.Add(new(Ability.Slide, SlideCondition));
    }

    protected override void ApplyEnterActions()
    {
        StartSpeed = Entity.Velocity.y;
        MoveDirection = 1;
        base.ApplyEnterActions();
        
        if (_clip != null)
        {
            Entity.Source.PlayOneShot(_clip);
        }
    }

    protected override void ApplyUpdateActions()
    {
        base.ApplyUpdateActions();
        Entity.SetVelocityY(MoveSpeed);
    }

    public void OnClimb()
    {
        if (_clip != null)
        {
            Entity.Source.PlayOneShot(_clip);
        }
    }
}
