using UnityEngine;

public sealed class PlayerGroundedState : PlayerState
{
    [SerializeField] private float _waitForLedgeClimbTime;

    private void Start()
    {
        bool InAirCondition() => !Player.Grounded;

        bool TouchingWallCondition() => Player.TouchingWall &&
                                        Player.TouchingLegde &&
                                        Player.IsStanding &&
                                        Player.Input.Grab &&
                                        Player.Input.Move.y != -1 &&
                                        !Player.AttackAbility.IsActive;


        bool OnLedgeCondition() => Player.OnLedgeState.LedgeClimbing;

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
        Transitions.Add(new(Player.TouchingWallState, TouchingWallCondition, TouchingWallAction));
        Transitions.Add(new(Player.OnLedgeState, OnLedgeCondition));
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

        Player.OnLedgeState.LedgeClimbing = false;

        Player.JumpAbility.RestoreJumps();
        Player.JumpAbility.CancelRequest();
        Player.DashAbility.RestoreDashes();

        Player.InAirState.TerminateTryingLedgeClimb();
    }

    protected override void ApplyUpdateActions()
    {
        base.ApplyUpdateActions();
        Player.InAirState.TryLedgeClimb();
    }
}
