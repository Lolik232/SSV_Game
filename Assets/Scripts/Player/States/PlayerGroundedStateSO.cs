using UnityEngine;

public class PlayerGroundedStateSO : PlayerStateSO
{
    [Header("Super State Transitions")]
    [SerializeField] private PlayerInAirStateSO _toInAirState;
    [SerializeField] private PlayerWallGrabStateSO _toWallGrabState;

    [Header("Dependent Abilities")]
    [SerializeField] private PlayerDashAbilitySO _dashAbility;
    [SerializeField] private PlayerJumpAbilitySO _jumpAbility;

    protected override void OnEnable()
    {
        base.OnEnable();
        transitions.Add(new TransitionItem(_toInAirState, () => !Player.isGrounded, () =>
        {
            if (Player.isTouchingCeiling)
            {
                Player.MoveToY(Player.transform.position.y - (Player.StandSize.y - Player.CrouchSize.y));
            }
            _jumpAbility.StartCoyoteTime();
        }));
        transitions.Add(new TransitionItem(_toWallGrabState, () => Player.isTouchingWall && Player.isTouchingLedge && !Player.isTouchingCeiling && Player.grabInput));

        enterActions.Add(() =>
        {
            _dashAbility.Unlock();
            _jumpAbility.Unlock();
        });

        checks.Add(() =>
        {
            Player.CheckIfGrounded();
            Player.CheckIfTouchingWall();
            Player.CheckIfTouchingLedge();
            Player.CheckIfTouchingCeiling();
        });
    }
}
