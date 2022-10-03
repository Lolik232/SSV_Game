using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInAirState", menuName = "State Machine/States/Player/States/In Air")]
public class PlayerInAirStateSO : PlayerStateSO
{
    [SerializeField] private PlayerLandStateSO _toLandState;
    [SerializeField] private PlayerWallGrabStateSO _toWallGrabState;
    [SerializeField] private PlayerWallSlideStateSO _toWallSlideState;
    [SerializeField] private PlayerJumpStateSO _toJumpState;
    [SerializeField] private PlayerWallJumpStateSO _toWallJumpState;
    [SerializeField] private PlayerLedgeHoldStateSO _toLedgeHoldState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toLandState, () => Player.isGrounded && Player.Velocity.y < 0.01f));
        transitions.Add(new TransitionItem(_toLedgeHoldState, () => Player.isTouchingWall && !Player.isTouchingLedge && !Player.isGroundClose));
        transitions.Add(new TransitionItem(_toJumpState, () => Player.jumpCoyoteTime && Player.jumpInput));
        transitions.Add(new TransitionItem(_toWallJumpState, () => ((Player.isTouchingWall ^ Player.isTouchingWallBack) || Player.wallJumpCoyoteTime) && Player.jumpInput));
        transitions.Add(new TransitionItem(_toWallGrabState, () => Player.isTouchingWall && Player.isTouchingLedge && Player.grabInput));
        transitions.Add(new TransitionItem(_toWallSlideState, () => Player.isTouchingWall && Player.moveInput.x == Player.facingDirection && Player.Velocity.y <= 0f));

        updateActions.Add(() =>
        {
            Player.CheckIfShouldFlip(Player.moveInput.x);
            if (!Player.wallJump)
            {
                Player.SetVelocityX(Player.moveInput.x * Player.InAirMoveSpeed);
            }
            SetFloat("xVelocity", Player.Velocity.x);
            SetFloat("yVelocity", Player.Velocity.y);
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
