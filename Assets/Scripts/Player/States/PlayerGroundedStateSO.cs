using UnityEngine;

public class PlayerGroundedStateSO : PlayerStateSO
{
    [SerializeField] private PlayerInAirStateSO _toInAirState;
    [SerializeField] private PlayerJumpStateSO _toJumpState;
    [SerializeField] private PlayerWallGrabStateSO _toWallGrabState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toJumpState, () => Player.jumpInput && !Player.isTouchingCeiling));
        transitions.Add(new TransitionItem(_toInAirState, () => !Player.isGrounded, () =>
        {
            if (Player.isTouchingCeiling)
            {
                Player.MoveToY(Player.transform.position.y - (Player.StandSize.y - Player.CrouchSize.y));
            }
            Player.jumpCoyoteTime = true;
            Player.coyoteTimeStart = Time.time;
        }));
        transitions.Add(new TransitionItem(_toWallGrabState, () => Player.isTouchingWall && Player.isTouchingLedge && !Player.isTouchingCeiling && Player.grabInput));

        checks.Add(() =>
        {
            Player.CheckIfGrounded();
            Player.CheckIfTouchingWall();
            Player.CheckIfTouchingCeiling();
        });
    }
}
