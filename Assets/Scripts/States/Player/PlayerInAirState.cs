﻿using System.Collections;

using UnityEngine;

public sealed class PlayerInAirState : PlayerState
{
    [SerializeField] private float _waitForLedgeClimbTime;

    public Coroutine JumpCoyoteTimeHolder
    {
        private get;
        set;
    }

    public Coroutine TryingLedgeClimbHolder
    {
        private get;
        set;
    }

    private void Start()
    {
        bool GroundedCondition() => Player.Grounded && Player.Velocity.y < 0.01f;

        bool TouchingWallCondition() => Player.TouchingWall &&
                                        Player.TouchingLegde &&
                                        (Player.Input.Grab || Player.Input.Move.x == Player.FacingDirection &&
                                        Player.Velocity.y < 0.01f);

        bool OnLedgeCondition() => Player.TouchingWall &&
                                   !Player.TouchingLegde &&
                                   Player.Input.Grab;

        void OnLedgeAction()
        {
            Player.OnLedgeState.DetermineLedgePosition();
        }

        void TouchingWallAction()
        {
            Player.TouchingWallState.DetermineWallPosition();
        }

        Transitions.Add(new(Player.GroundedState, GroundedCondition));
        Transitions.Add(new(Player.OnLedgeState, OnLedgeCondition, OnLedgeAction));
        Transitions.Add(new(Player.TouchingWallState, TouchingWallCondition, TouchingWallAction));
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        Player.MoveHorizontalAbility.Permited = true;
        Player.MoveVerticalAbility.Permited = false;
        Player.LedgeClimbAbility.Permited = false;
        Player.CrouchAbility.Permited = false;
        Player.JumpAbility.Permited = true;
        Player.DashAbility.Permited = true;
        Player.AttackAbility.Permited = true;
    }

    protected override void ApplyUpdateActions()
    {
        base.ApplyUpdateActions();

        if (Player.TouchingWall || Player.TouchingWallBack)
        {
            Player.JumpAbility.Request(Player.JumpAbility.WallJump);
        }
        else
        {
            Player.JumpAbility.CancelRequest();
        }
    }

    public void CheckJumpCoyoteTime()
    {
        if (JumpCoyoteTimeHolder != null)
        {
            StopCoroutine(JumpCoyoteTimeHolder);
        }

        JumpCoyoteTimeHolder = StartCoroutine(CheckJumpCoyoteTimeRoutine());
    }

    private IEnumerator CheckJumpCoyoteTimeRoutine()
    {
        yield return new WaitUntil(() => IsActive);

        while (IsActive && ActiveTime < Player.JumpAbility.NormalJump.CoyoteTime)
        {
            yield return null;

            if (Player.JumpAbility.IsActive)
            {
                yield break;
            }
        }

        if (IsActive)
        {
            Player.JumpAbility.SetJumpsEmpty();
            Player.JumpAbility.CancelRequest();
        }
    }
}
