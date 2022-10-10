using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerTouchingWallStateSO : PlayerStateSO
{
    [Header("Super State Transitions")]
    [SerializeField] private PlayerIdleStateSO _toIdleState;
    [SerializeField] private PlayerInAirStateSO _toInAirState;
    [SerializeField] private PlayerLedgeHoldStateSO _toLedgeHoldState;

    [Header("Dependent Abilities")]
    [SerializeField] private PlayerDashAbilitySO _dashAbility;
    [SerializeField] private PlayerJumpAbilitySO _jumpAbility;
    [SerializeField] private PlayerWallJumpAbilitySO _wallJumpAbility;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toIdleState, () => Player.isGrounded && (!Player.grabInput || Player.moveInput.y < 0f || !Player.isTouchingWall)));
        transitions.Add(new TransitionItem(_toInAirState, () => !Player.isTouchingWall || (Player.moveInput.x != Player.facingDirection && !Player.grabInput),
            () =>
            {
                _wallJumpAbility.StartCoyoteTime();
            }));
        transitions.Add(new TransitionItem(_toLedgeHoldState, () => Player.isTouchingWall && !Player.isTouchingLedge && !Player.isGroundClose));


        enterActions.Add(() =>
        {
            Player.MoveToX(Player.wallPosition.x + Player.wallDirection * (Player.Collider.size.x / 2 + 0.02f));
            _dashAbility.Cache();
            _dashAbility.Block();
            _jumpAbility.Block();
            _wallJumpAbility.Unlock();
        });

        exitActions.Add(() =>
        {
            _dashAbility.Restore();
            _wallJumpAbility.Block();
        });
    }
}
