using UnityEngine;

public class PlayerOnLedgeState : PlayerState
{
    private Vector2 _startPosition;
    private Vector2 _endPosition;
    private int _wallDirection;

    protected override void Start()
    {
        base.Start();

        bool GroundedCondition() => !Player.LedgeClimbAbility.IsActive;

        Transitions.Add(new(Player.GroundedState, GroundedCondition));
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        Player.MoveHorizontalAbility.Permited = false;
        Player.MoveVerticalAbility.Permited = false;
        Player.LedgeClimbAbility.Permited = true;
        Player.CrouchAbility.Permited = false;
        Player.JumpAbility.Permited = false;
        Player.DashAbility.Permited = false;
        Player.AttackAbility.Permited = false;

        Player.SetPosition(_startPosition);
        Player.SetVelocity(Vector2.zero);
        Player.RotateIntoDirection(-_wallDirection);
        Player.SetGravity(0f);
        Player.BlockRotation();
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        Player.SetPosition(_endPosition);
        Player.ResetGravity();
        Player.UnlockRotation();

        Player.CrouchAbility.Request(Player.CrouchAbility.Crouch);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(_startPosition + Player.Offset, Player.Size);
        Gizmos.DrawWireCube(_endPosition + Player.Offset, Player.Size);
    }

    public void DetermineLedgePosition()
    {
        _startPosition = new Vector2(Player.WallPosition.x + Player.WallDirection * (Player.Size.x / 2 + IChecker.CHECK_OFFSET),
                                                                     Player.GroundPosition.y - 1f);
        _endPosition = new Vector2(Player.WallPosition.x - Player.WallDirection * (Player.Size.x / 2 + IChecker.CHECK_OFFSET),
                                                                 Player.GroundPosition.y + IChecker.CHECK_OFFSET);
        _wallDirection = Player.WallDirection;
    }
}
