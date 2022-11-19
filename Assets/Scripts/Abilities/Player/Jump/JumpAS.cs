using UnityEngine;

public class JumpAS : AbilityState<JumpAbility>
{
    [SerializeField] private float _coyoteTime;
    [SerializeField] private float _jumpForse;

    public float CoyoteTime
    {
        get => _coyoteTime;
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        if (Ability.AmountOfJumps == 0)
        {
            Ability.OnExit();
            return;
        }

        Ability.DecreaseJumps();
        Entity.SetVelocityY(_jumpForse);
    }

    protected override void ApplyUpdateActions()
    {
        base.ApplyUpdateActions();

        if (!Entity.Behaviour.Jump || Entity.Velocity.y < 0.01f)
        {
            if (Entity.Velocity.y > 0f)
            {
                Entity.SetVelocityY(Entity.Velocity.y / 2);
            }

            Ability.OnExit();
        }
    }
}
