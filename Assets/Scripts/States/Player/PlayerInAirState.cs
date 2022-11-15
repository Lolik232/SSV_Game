using System.Collections;

using UnityEngine;

public sealed class PlayerInAirState : State
{
    [SerializeField] private float _waitForLedgeClimbTime;

    private Player _player;

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

    protected override void Awake()
    {
        base.Awake();
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        bool GroundedCondition() => _player.Grounded && _player.Velocity.y < 0.01f;

        bool TouchingWallCondition() => _player.TouchingWall &&
                                        _player.TouchingLegde &&
                                        (_player.Input.Grab || _player.Input.Move.x == _player.FacingDirection &&
                                        _player.Velocity.y < 0.01f);

        Transitions.Add(new(_player.GroundedState, GroundedCondition));
        Transitions.Add(new(_player.TouchingWallState, TouchingWallCondition));
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        _player.MoveHorizontalAbility.Permited = true;
        _player.MoveVerticalAbility.Permited = false;
        _player.LedgeClimbAbility.Permited = false;
        _player.CrouchAbility.Permited = false;
        _player.JumpAbility.Permited = true;
        _player.DashAbility.Permited = true;
        _player.AttackAbility.Permited = true;
    }

    protected override void ApplyUpdateActions()
    {
        base.ApplyUpdateActions();
        TryLedgeClimb();

        if (_player.TouchingWall || _player.TouchingWallBack)
        {
            _player.JumpAbility.Request(_player.JumpAbility.WallJump);
        }
        else
        {
            _player.JumpAbility.CancelRequest();
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

        while (IsActive && ActiveTime < _player.JumpAbility.NormalJump.CoyoteTime)
        {
            yield return null;

            if (_player.JumpAbility.IsActive)
            {
                yield break;
            }
        }

        if (IsActive)
        {
            _player.JumpAbility.SetJumpsEmpty();
            _player.JumpAbility.CancelRequest();
        }
    }

    public void TryLedgeClimb()
    {
        if (!_tryingLedgeClimb && _player.TouchingWall && !_player.TouchingLegde)
        {
            TryingLedgeClimbTime = Time.time;
            _player.OnLedgeState.DetermineLedgePosition();
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
        return _player.TouchingWall &&
                (_player.Input.Move.x == _player.FacingDirection ||
                _player.Input.Move.y == 1 || _player.Input.Grab);
    }

    private IEnumerator CheckOnLedgeCondition()
    {
        _player.JumpAbility.Permited = false;

        while (_tryingLedgeClimb = CheckIfTryingLedgeClimb())
        {
            if (TryingLedgeClimbTime > _waitForLedgeClimbTime / Mathf.Max(Mathf.Abs(_player.Velocity.y), 1f))
            {
                Machine.GetTransition(_player.OnLedgeState);
                _tryingLedgeClimb = false;
                yield break;
            }

            yield return null;
        }

        _player.JumpAbility.Permited = true;
    }
}
