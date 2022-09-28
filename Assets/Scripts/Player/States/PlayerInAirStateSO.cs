using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInAirState", menuName = "State Machine/States/Player/Sub States/In Air")]
public class PlayerInAirStateSO : PlayerStateSO
{
    private bool _isGrounded;
    private bool _isGroundClose;
    private bool _isTouchingWall;
    private bool _isTouchingLedge;

    [SerializeField] private PlayerLandStateSO _toLandState;
    [SerializeField] private PlayerWallGrabStateSO _toWallGrabState;
    [SerializeField] private PlayerWallSlideStateSO _toWallSlideState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toLandState, () => _isGrounded && Player.Velocity.y < 0.01f));
        transitions.Add(new TransitionItem(_toWallGrabState, () => _isTouchingWall && _isTouchingLedge && Player.grabInput));
        transitions.Add(new TransitionItem(_toWallSlideState, () => _isTouchingWall && Player.moveInput.x == Player.facingDirection && Player.Velocity.y <= 0f));

        updateActions.Add(() =>
        {
            Player.CheckIfShouldFlip(Player.moveInput.x);
            Player.SetVelocityX(Player.moveInput.x * Player.InAirMoveSpeed);
            SetFloat("xVelocity", Player.Velocity.x);
            SetFloat("yVelocity", Player.Velocity.y);
        });

        checks.Add(() =>
        {
            _isGrounded = Player.CheckIfGrounded();
            _isGroundClose = Player.CheckIfGroundClose();
            _isTouchingWall = Player.CheckIfTouchingWall();
            _isTouchingLedge = Player.CheckIfTouchingLedge();
        });
    }
}
