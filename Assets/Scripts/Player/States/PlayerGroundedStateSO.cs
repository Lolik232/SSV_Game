using UnityEngine;

public class PlayerGroundedStateSO : PlayerStateSO
{
    [SerializeField] private PlayerInAirStateSO _toInAirState;
    [SerializeField] private PlayerJumpStateSO _toJumpState;
    [SerializeField] private PlayerWallGrabStateSO _toWallGrabState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toJumpState, () => Player.jumpInput));
        transitions.Add(new TransitionItem(_toInAirState, () => !Player.isGrounded));
        transitions.Add(new TransitionItem(_toWallGrabState, () => Player.isTouchingWall && Player.grabInput));

        exitActions.Add(()=>
        {
            Player.jumpCoyoteTime = !Player.isGrounded && !Player.jumpInput;
        });
    }
}
