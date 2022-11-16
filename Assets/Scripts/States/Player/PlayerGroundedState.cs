using UnityEngine;

public sealed class PlayerGroundedState : State
{
    [SerializeField] private float _waitForLedgeClimbTime;

    private Player _player;

    protected override void Awake()
    {
        base.Awake();
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        bool InAirCondition() => !_player.Grounded;

        bool TouchingWallCondition() => _player.TouchingWall &&
                                        _player.TouchingLegde &&
                                        _player.IsStanding &&
                                        _player.Input.Grab &&
                                        _player.Input.Move.y != -1;

        bool OnLedgeCondition() => _player.OnLedgeState.LedgeClimbing;

        void InAirAction()
        {
            if (!_player.IsStanding && _player.TouchingCeiling)
            {
                _player.SetPositionY(_player.Position.y - (_player.StandSize.y - _player.CrouchSize.y));
            }

            if (!_player.JumpAbility.IsActive)
            {
                _player.InAirState.CheckJumpCoyoteTime();
            }
        }

        void TouchingWallAction()
        {
            _player.TouchingWallState.DetermineWallPosition();
        }

        Transitions.Add(new(_player.InAirState, InAirCondition, InAirAction));
        Transitions.Add(new(_player.TouchingWallState, TouchingWallCondition, TouchingWallAction));
        Transitions.Add(new(_player.OnLedgeState, OnLedgeCondition));
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        _player.MoveHorizontalAbility.Permited = true;
        _player.MoveVerticalAbility.Permited = false;
        _player.LedgeClimbAbility.Permited = false;
        _player.CrouchAbility.Permited = true;
        _player.JumpAbility.Permited = true;
        _player.DashAbility.Permited = true;
        _player.AttackAbility.Permited = true;

        _player.OnLedgeState.LedgeClimbing = false;

        _player.JumpAbility.RestoreJumps();
        _player.JumpAbility.CancelRequest();
        _player.DashAbility.RestoreDashes();

        _player.InAirState.TerminateTryingLedgeClimb();
    }

    protected override void ApplyUpdateActions()
    {
        base.ApplyUpdateActions();
        _player.InAirState.TryLedgeClimb();
    }
}
