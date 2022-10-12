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
        transitions.Add(new TransitionItem(_toInAirState, () => !Player.isGrounded || _jumpAbility.isActive || _dashAbility.isActive, () =>
        {
            if (Player.isTouchingCeiling)
            {
                Player.MoveToY(Player.transform.position.y - (Player.StandSize.y - Player.CrouchSize.y));
            }
            if (!_jumpAbility.isActive)
            {
                _jumpAbility.StartCoyoteTime();
            }
        }));
        transitions.Add(new TransitionItem(_toWallGrabState, () => Player.isTouchingWall && Player.isTouchingLedge && !Player.isTouchingCeiling && Player.grabInput && Player.moveInput.y >= 0f));

        enterActions.Add(() =>
        {
            _dashAbility.RestoreAmountOfUsages();
            _jumpAbility.RestoreAmountOfUsages();
        });
    }
}
