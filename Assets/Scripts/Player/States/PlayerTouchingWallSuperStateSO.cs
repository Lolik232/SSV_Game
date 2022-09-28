using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerTouchingWallSuperState", menuName = "State Machine/States/Player/Super States/Touching Wall")]
public class PlayerTouchingWallSuperStateSO : PlayerSuperStateSO
{
    private bool _isGrounded;
    private bool _isGroundClose;
    private bool _isTouchingWall;
    private bool _isTouchingLedge;

    [SerializeField] private PlayerIdleStateSO _toIdleState;
    [SerializeField] private PlayerInAirStateSO _toInAirState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toIdleState, () => _isGrounded && !Player.grabInput));
        transitions.Add(new TransitionItem(_toInAirState, () => !_isTouchingWall || (Player.moveInput.x != Player.facingDirection && !Player.grabInput)));

        checks.Add(() =>
        {
            _isGrounded = Player.CheckIfGrounded();
            _isGroundClose = Player.CheckIfGroundClose();
            _isTouchingWall = Player.CheckIfTouchingWall();
            _isTouchingLedge = Player.CheckIfTouchingLedge();
        });
    }
}
