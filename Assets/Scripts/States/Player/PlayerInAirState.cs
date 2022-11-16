using System.Collections;

using UnityEngine;

public sealed class PlayerInAirState : PlayerState
{
    [SerializeField] private float _waitForLedgeClimbTime;

    private bool _tryingLedgeClimb;
    private float _tryingLedgeClimbStartTime;

    private float TryingLedgeClimbTime
    {
        get => Time.time - _tryingLedgeClimbStartTime;
        set => _tryingLedgeClimbStartTime = value;
    }

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

        bool OnLedgeCondition() => Player.OnLedgeState.LedgeClimbing;

        void TouchingWallAction() {
            Player.TouchingWallState.DetermineWallPosition();
        }

        Transitions.Add(new(Player.GroundedState, GroundedCondition));
        Transitions.Add(new(Player.TouchingWallState, TouchingWallCondition, TouchingWallAction));
        Transitions.Add(new(Player.OnLedgeState, OnLedgeCondition));
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

        Player.OnLedgeState.LedgeClimbing = false;
    }

    protected override void ApplyUpdateActions()
    {
        base.ApplyUpdateActions();
        TryLedgeClimb();

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

    public void TryLedgeClimb()
    {
        if (!_tryingLedgeClimb && Player.TouchingWall && !Player.TouchingLegde)
        {
            TryingLedgeClimbTime = Time.time;
            Player.OnLedgeState.DetermineLedgePosition();
            TerminateTryingLedgeClimb();
            TryingLedgeClimbHolder = StartCoroutine(CheckOnLedgeCondition());
        }
    }

    public void TerminateTryingLedgeClimb()
    {
        if (TryingLedgeClimbHolder != null)
        {
            StopCoroutine(TryingLedgeClimbHolder);
            _tryingLedgeClimb = false;
        }
    }

    private bool CheckIfTryingLedgeClimb()
    {
        return Player.TouchingWall &&
                (Player.Input.Move.x == Player.FacingDirection ||
                Player.Input.Move.y == 1 || Player.Input.Grab);
    }

    private IEnumerator CheckOnLedgeCondition()
    {
        Player.JumpAbility.Permited = false;
        _tryingLedgeClimb = true;

        yield return new WaitUntil(() => TryingLedgeClimbTime > _waitForLedgeClimbTime / Mathf.Max(Mathf.Abs(Player.Velocity.y), 1f));

        if (CheckIfTryingLedgeClimb())
        {
            Player.OnLedgeState.LedgeClimbing = true;
        }
        else
        {
            Player.JumpAbility.Permited = true;
        }

        _tryingLedgeClimb = false;
    }
}
