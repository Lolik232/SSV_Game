using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerTouchingWallStateSO : PlayerStateSO
{
    [SerializeField] private PlayerIdleStateSO _toIdleState;
    [SerializeField] private PlayerInAirStateSO _toInAirState;
    [SerializeField] private PlayerWallJumpStateSO _toWallJumpState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toWallJumpState, () => !Player.isTouchingWallBack && Player.jumpInput));
        transitions.Add(new TransitionItem(_toIdleState, () => Player.isGrounded && (!Player.grabInput || !Player.isTouchingWall)));
        transitions.Add(new TransitionItem(_toInAirState, () => !Player.isTouchingWall || (Player.moveInput.x != Player.facingDirection && !Player.grabInput)));

        enterActions.Add(() =>
        {
            Player.MoveToX(Player.wallPosition.x + Player.wallDirection * (Player.RbSize.x / 2 + 0.02f));
        });

        exitActions.Add(() =>
        {
            Player.wallJumpCoyoteTime = !Player.isTouchingWall && !Player.isTouchingWallBack && !Player.isGrounded && !Player.jumpInput;
        }); 
    }
}
