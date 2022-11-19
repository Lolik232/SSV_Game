using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    private Vector2 _holdPosition;
    private int _wallDirection;

    protected override void Start()
    {
        base.Start();
        bool GroundedCondition() => Player.Grounded && (Player.Behaviour.Move.y == -1 || !Player.Behaviour.Grab || Player.Behaviour.Attack);

        bool InAirCondition() => !Player.TouchingWall || (!Player.Behaviour.Grab && Player.Behaviour.Move.x != Player.FacingDirection) || Player.Behaviour.Dash;

        bool OnLedgeCondition() => Player.TouchingWall && !Player.TouchingLedge;

        void OnLedgeAction()
        {
            Player.OnLedgeState.DetermineLedgePosition();
        }

        void InAirAction()
        {
            if (!Player.JumpAbility.IsActive)
            {
                Player.InAirState.CheckJumpCoyoteTime();
            }
        }

        Transitions.Add(new(Player.GroundedState, GroundedCondition));
        Transitions.Add(new(Player.OnLedgeState, OnLedgeCondition, OnLedgeAction));
        Transitions.Add(new(Player.InAirState, InAirCondition, InAirAction));
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        Player.MoveHorizontalAbility.Permited = false;
        Player.MoveVerticalAbility.Permited = true;
        Player.LedgeClimbAbility.Permited = false;
        Player.CrouchAbility.Permited = false;
        Player.JumpAbility.Permited = true;
        Player.DashAbility.Permited = true;
        Player.AttackAbility.Permited = true;

        Player.JumpAbility.Request(Player.JumpAbility.WallJump);
        Player.SetPosition(_holdPosition);
        Player.RotateIntoDirection(-_wallDirection);
        Player.SetGravity(0f);
        Player.BlockRotation();
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        Player.ResetGravity();
        Player.UnlockRotation();
    }

    public void DetermineWallPosition()
    {
        _holdPosition = new Vector2(Player.WallPosition.x + Player.WallDirection * (Player.Size.x / 2 + IChecker.CHECK_OFFSET), Player.Position.y);
        _wallDirection = Player.WallDirection;
    }
}
