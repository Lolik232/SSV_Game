using UnityEngine;


[CreateAssetMenu(fileName = "PlayerGroundedSuperState", menuName = "State Machine/States/Player/Super States/Grounded")]
public class PlayerGroundedSuperStateSO : PlayerSuperStateSO
{
    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _isTouchingLedge;

    [SerializeField] private PlayerInAirStateSO _toInAirState;
    [SerializeField] private PlayerJumpStateSO _toJumpState;
    [SerializeField] private PlayerWallGrabStateSO _toWallGrabState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toJumpState, () => Player.jumpInput));
        transitions.Add(new TransitionItem(_toInAirState, () => !_isGrounded));
        transitions.Add(new TransitionItem(_toWallGrabState, () => _isTouchingWall && _isTouchingLedge && Player.grabInput));

        checks.Add(() =>
        {
            _isGrounded = Player.CheckIfGrounded();
            _isTouchingWall = Player.CheckIfTouchingWall();
            _isTouchingLedge = Player.CheckIfTouchingLedge();
        });
    }
}
