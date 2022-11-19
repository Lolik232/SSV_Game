using UnityEngine;

public sealed class PlayerGroundedState : PlayerState
{
    [SerializeField] private float _waitForLedgeClimbTime;

    protected override void Start()
    {
        base.Start();
        bool InAirCondition() => !Player.Grounded || Player.Behaviour.Dash;

        bool TouchingWallCondition() => Player.TouchingWall &&
                                        Player.TouchingLedge &&
                                        Player.IsStanding &&
                                        Player.Behaviour.Grab &&
                                        Player.Behaviour.Move.y != -1 &&
                                        !Player.AttackAbility.IsActive;

        bool OnLedgeCondition() => Player.TouchingWall && !Player.TouchingLedge && Player.Behaviour.Grab;

        void OnLedgeAction()
        {
            Player.OnLedgeState.DetermineLedgePosition();
        }

        void InAirAction()
        {
            if (!Player.IsStanding && Player.TouchingCeiling)
            {
                Player.SetPositionY(Player.Position.y - (Player.StandSize.y - Player.CrouchSize.y));
            }

            if (!Player.JumpAbility.IsActive)
            {
                Player.InAirState.CheckJumpCoyoteTime();
            }
        }

        void TouchingWallAction()
        {
            Player.TouchingWallState.DetermineWallPosition();
        }

        Transitions.Add(new(Player.InAirState, InAirCondition, InAirAction));
        Transitions.Add(new(Player.OnLedgeState, OnLedgeCondition, OnLedgeAction));
        Transitions.Add(new(Player.TouchingWallState, TouchingWallCondition, TouchingWallAction));
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        Player.MoveHorizontalAbility.Permited = true;
        Player.MoveVerticalAbility.Permited = false;
        Player.LedgeClimbAbility.Permited = false;
        Player.CrouchAbility.Permited = true;
        Player.JumpAbility.Permited = true;
        Player.DashAbility.Permited = true;
        Player.AttackAbility.Permited = true;

        Player.JumpAbility.RestoreJumps();
        Player.JumpAbility.CancelRequest();
        Player.DashAbility.RestoreDashes();
    }
}
