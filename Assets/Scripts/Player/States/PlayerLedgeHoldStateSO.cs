using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLedgeHoldState", menuName = "State Machine/States/Player/States/Ledge Hold")]

public class PlayerLedgeHoldStateSO : PlayerStateSO
{
    private bool _isHanging;

    [SerializeField] private PlayerWallJumpStateSO _toWallJumpState;
    [SerializeField] private PlayerInAirStateSO _toInAirState;
    [SerializeField] private PlayerLedgeClimbStateSO _toLedgeClimbState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toWallJumpState, () => _isHanging && !Player.isTouchingWallBack && Player.jumpInput));
        transitions.Add(new TransitionItem(_toLedgeClimbState, () => _isHanging && (Player.moveInput.x == Player.facingDirection || Player.moveInput.y == 1)));
        transitions.Add(new TransitionItem(_toInAirState, () => _isHanging && (Player.moveInput.x == -Player.facingDirection || Player.moveInput.y == -1)));

        enterActions.Add(() =>
        {
            _isHanging = false;
            Player.ledgeStartPosition = new Vector2(Player.cornerPosition.x + Player.wallDirection * (Player.StartLedgeOffset.x + 0.02f),
                                                    Player.cornerPosition.y - Player.StartLedgeOffset.y - 0.02f);
            Player.ledgeEndPosition = new Vector2(Player.cornerPosition.x - Player.wallDirection * Player.EndLedgeOffset.x,
                                                  Player.cornerPosition.y + Player.EndLedgeOffset.y + 0.02f);
            Player.HoldPosition(Player.ledgeStartPosition);
        });

        updateActions.Add(() =>
        {
            Player.HoldPosition(Player.ledgeStartPosition);
        });

        animationFinishActions.Add(() =>
        {
            _isHanging = true;
        });

        checks.Add(() =>
        {
            Player.CheckIfTouchingWallBack();
        });
    }
}
