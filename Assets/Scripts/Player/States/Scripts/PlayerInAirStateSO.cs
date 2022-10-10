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

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toLandState, () => Player.isGrounded && Player.Rb.velocity.y < 0.01f));
        transitions.Add(new TransitionItem(_toLedgeHoldState, () => Player.isTouchingWall && !Player.isTouchingLedge && !Player.isGroundClose));
        transitions.Add(new TransitionItem(_toWallGrabState, () => Player.isTouchingWall && Player.isTouchingLedge && Player.grabInput));
        transitions.Add(new TransitionItem(_toWallSlideState, () => Player.isTouchingWall && Player.moveInput.x == Player.facingDirection && Player.Rb.velocity.y <= 0f));

        updateActions.Add(() =>
        {
            _jumpAbility.SetAble(_jumpAbility.CoyoteTime);
            _wallJumpAbility.SetAble(Player.isTouchingWall || Player.isTouchingWallBack || _wallJumpAbility.CoyoteTime);

            if (_jumpAbility.isActive && !Player.jumpInputHold && Player.Rb.velocity.y > 0f)
            {
                Player.SetVelocityY(Player.Rb.velocity.y * 0.5f);
                _jumpAbility.Terminate();
            }
            if (!_wallJumpAbility.isActive)
            {
                Player.SetVelocityX(Player.moveInput.x * Player.InAirMoveSpeed);
                Player.CheckIfShouldFlip(Player.moveInput.x);
            }
            Anim.SetFloat("xVelocity", Player.Rb.velocity.x);
            Anim.SetFloat("yVelocity", Player.Rb.velocity.y);
        });
    }
}
