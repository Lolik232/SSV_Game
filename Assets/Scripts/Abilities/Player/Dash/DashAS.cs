using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TrailRenderer))]


public class DashAS : AbilityState<DashAbility>
{
    [SerializeField] private float _duration;
    [SerializeField] private float _dashForse;

    private PlayerInput _playerInput;
    private TrailRenderer _tr;

    protected override void Awake()
    {
        base.Awake();
        _tr = GetComponent<TrailRenderer>();
        _playerInput = GetComponent<PlayerInput>();
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        Vector2 dashDirection;
        if (_playerInput.currentControlScheme == "Keyboard")
        {
            dashDirection = (Entity.Behaviour.LookAt - Entity.Center).normalized;
        }
        else if (Entity.Behaviour.Move != Vector2Int.zero)
        {
            dashDirection = Entity.Behaviour.Move;
        }
        else
        {
            dashDirection = Entity.FacingDirection * Vector2.right;
        }

        Entity.SetVelocity(_dashForse * dashDirection);
        Entity.RotateIntoDirection(dashDirection.x > 0 ? 1 : -1);
        Entity.BlockVelocity();

        if (_clip != null)
        {
            Entity.Source.PlayOneShot(_clip);
        }

        _tr.Clear();
        _tr.emitting = true;
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        Entity.UnlockVelocity();

        _tr.emitting = false;
    }

    protected override void ApplyUpdateActions()
    {
        base.ApplyUpdateActions();

        if (ActiveTime > _duration || Entity.Velocity.magnitude < _dashForse / 2)
        {
            if (Entity.Velocity.y > 0f)
            {
                Entity.SetVelocityY(Entity.Velocity.y / 5);
            }

            Ability.OnExit();
        }
    }
}
