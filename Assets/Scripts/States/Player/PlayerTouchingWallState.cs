using UnityEngine;

public class PlayerTouchingWallState : State
{
    private  Player _player;

    protected override void Awake()
    {
        base.Awake();
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        bool GroundedCondition() => _player.Grounded && (_player.Input.Move.y == -1 || !_player.Input.Grab);

        bool InAirCondition() => !_player.TouchingWall || (!_player.Input.Grab && _player.Input.Move.x != _player.FacingDirection);

        bool OnLedgeCondition() => _player.TouchingWall && !_player.TouchingLegde;

        void OnLedgeAction()
        {
            _player.OnLedgeState.DetermineLedgePosition();
        }

        void InAirAction()
        {
            if (!_player.JumpAbility.IsActive)
            {
                _player.InAirState.CheckJumpCoyoteTime();
            }
        }

        Transitions.Add(new(_player.GroundedState, GroundedCondition));
        Transitions.Add(new(_player.InAirState, InAirCondition, InAirAction));
        Transitions.Add(new(_player.OnLedgeState, OnLedgeCondition, OnLedgeAction));
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        _player.MoveHorizontalAbility.Permited = false;
        _player.MoveVerticalAbility.Permited = true;
        _player.LedgeClimbAbility.Permited = false;
        _player.CrouchAbility.Permited = false;
        _player.JumpAbility.Permited = true;
        _player.DashAbility.Permited = true;
        _player.AttackAbility.Permited = true;

        _player.JumpAbility.Request(_player.JumpAbility.WallJump);

        var holdPosition = new Vector2(_player.WallPosition.x + _player.WallDirection * (_player.Size.x / 2 + IChecker.CHECK_OFFSET), _player.Position.y);
        _player.SetPosition(holdPosition);
        _player.SetGravity(0f);
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        _player.ResetGravity();
    }
}
