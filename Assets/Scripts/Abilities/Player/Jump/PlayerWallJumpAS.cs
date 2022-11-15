using UnityEngine;

public class PlayerWallJumpAS : AbilityState<PlayerJumpAbility>
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
        Ability.Player.BlockVelocity();
        Ability.Player.SetVelocity(_jumpForse, _angle, Ability.Player.WallDirection);
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        Ability.Player.UnlockVelocity();
    }

    protected override void ApplyUpdateActions()
    {
        base.ApplyUpdateActions();

        if (ActiveTime > _duration || Mathf.Abs(Ability.Player.Velocity.x) < 0.01f)
        {
            if (Ability.Player.Velocity.y > 0f)
            {
                Ability.Player.SetVelocityY(Ability.Player.Velocity.y / 2);
            }

            Ability.OnExit();
        }
    }
}
