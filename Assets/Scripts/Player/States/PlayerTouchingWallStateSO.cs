using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerTouchingWallStateSO : PlayerStateSO
{
    [SerializeField] private PlayerIdleStateSO _toIdleState;
    [SerializeField] private PlayerInAirStateSO _toInAirState;
    [SerializeField] private PlayerWallJumpStateSO _toWallJumpState;
    [SerializeField] private PlayerLedgeHoldStateSO _toLedgeHoldState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toWallJumpState, () => !Player.isTouchingWallBack && Player.jumpInput));
        transitions.Add(new TransitionItem(_toIdleState, () => Player.isGrounded && (!Player.grabInput || !Player.isTouchingWall)));
        transitions.Add(new TransitionItem(_toInAirState, () => !Player.isTouchingWall || (Player.moveInput.x != Player.facingDirection && !Player.grabInput),
            () =>        
            {
                Player.wallJumpCoyoteTime = true;
                Player.coyoteTimeStart = Time.time;
            }));
        transitions.Add(new TransitionItem(_toLedgeHoldState, () => Player.isTouchingWall && !Player.isTouchingLedge && !Player.isGroundClose));


        enterActions.Add(() =>
        {
            Player.MoveToX(Player.wallPosition.x + Player.wallDirection * (Player.Size.x / 2 + 0.02f));
        });

        checks.Add(() =>
        {
            Player.CheckIfGrounded();
            Player.CheckIfGroundClose();
            Player.CheckIfTouchingWall();
            Player.CheckIfTouchingWallBack();
            Player.CheckIfTouchingLedge();
            if (Player.isTouchingWall && !Player.isTouchingLedge && !Player.isGroundClose)
            {
                Player.DeterminCornerPosition();
            }
        });
    }
}
