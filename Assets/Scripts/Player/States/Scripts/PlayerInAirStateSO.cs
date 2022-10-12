using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInAirState", menuName = "Player/States/In Air")]
public class PlayerInAirStateSO : PlayerStateSO
{
    [Header("State Transitions")]
    [SerializeField] private PlayerLandStateSO _toLandState;
    [SerializeField] private PlayerWallGrabStateSO _toWallGrabState;
    [SerializeField] private PlayerWallSlideStateSO _toWallSlideState;
    [SerializeField] private PlayerLedgeHoldStateSO _toLedgeHoldState;

    [Header("Dependent Abilities")]
    [SerializeField] private PlayerJumpAbilitySO _jumpAbility;
    [SerializeField] private PlayerWallJumpAbilitySO _wallJumpAbility;
    [SerializeField] private PlayerDashAbilitySO _dashAbility;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toLandState, () => Player.isGrounded && Player.Rb.velocity.y < 0.01f));
        transitions.Add(new TransitionItem(_toLedgeHoldState, () => Player.isTouchingWall && !Player.isTouchingLedge && !Player.isGroundClose));
        transitions.Add(new TransitionItem(_toWallGrabState, () => Player.isTouchingWall && Player.isTouchingLedge && Player.grabInput));
        transitions.Add(new TransitionItem(_toWallSlideState, () => Player.isTouchingWall && Player.moveInput.x == Player.facingDirection && Player.Rb.velocity.y <= 0f));

        updateActions.Add(() =>
        {
            if (!_jumpAbility.CoyoteTime)
            {
                _jumpAbility.SetZeroAmountOfUsages();
            }
            if (Player.isTouchingWall || Player.isTouchingWallBack)
            {
                _wallJumpAbility.RestoreAmountOfUsages();
            }
            else if (!_wallJumpAbility.CoyoteTime)
            {
                _wallJumpAbility.SetZeroAmountOfUsages();
            }

            Player.TrySetVelocityX(Player.moveInput.x * Player.InAirMoveSpeed);
            Player.CheckIfShouldFlip(Player.moveInput.x);

            Anim.SetFloat("xVelocity", Player.Rb.velocity.x);
            Anim.SetFloat("yVelocity", Player.Rb.velocity.y);
        });
    }
}
