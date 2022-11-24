using UnityEngine;

public class WallJumpAS : AbilityState<JumpAbility>
{
    [SerializeField] private float _coyoteTime;
    [SerializeField] private float _jumpForse;
    [SerializeField] private Vector2 _angle;
    [SerializeField] private float _duration;

    public float CoyoteTime
    {
        get => _coyoteTime;
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        Ability.RestoreJumps();
        Ability.DecreaseJumps();

        Entity.BlockVelocity();
        Entity.RotateIntoDirection(Entity.WallDirection);
        Entity.SetVelocity(_jumpForse, _angle, Entity.WallDirection);
        
        if (_clip != null)
        {
            Entity.Source.PlayOneShot(_clip);
        }
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        Entity.UnlockVelocity();
    }

    protected override void ApplyUpdateActions()
    {
        base.ApplyUpdateActions();

        if (ActiveTime > _duration || Mathf.Abs(Entity.Velocity.x) < 0.01f)
        {
            if (Entity.Velocity.y > 0f)
            {
                Entity.SetVelocityY(Entity.Velocity.y / 2);
            }

            Ability.OnExit();
        }
    }
}
